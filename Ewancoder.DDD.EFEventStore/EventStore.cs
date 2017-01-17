namespace Ewancoder.DDD.EFEventStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;
    using DDD.Exceptions;
    using Interfaces;
    using Services;
    using Exceptions;

    /// <summary>
    /// Entity framework driven Event Store.
    /// This event store will only work with event implementing abstract
    /// DomainEvent class.
    /// </summary>
    public sealed class EventStore : IEventStore
    {
        /// <summary>
        /// Used to serialize and deserialize event data.
        /// </summary>
        private readonly IEventSerializer _serializer;

        /// <summary>
        /// Used to update events to recent versions.
        /// </summary>
        private readonly IEventUpdater _updater;

        /// <summary>
        /// Used to resolve snapshot type identifier.
        /// </summary>
        private readonly IEventIdentifierFactory _eventIdentifierFactory;

        /// <summary>
        /// Used to get current UTC time.
        /// </summary>
        private readonly ITimeService _time;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStore"/> class.
        /// </summary>
        /// <param name="serializer">Data serializer.</param>
        /// <param name="updater">Event updater.</param>
        /// <param name="eventIdentifierFactory">Snapshot identifier factory.</param>
        public EventStore(
            IEventSerializer serializer,
            IEventUpdater updater,
            IEventIdentifierFactory eventIdentifierFactory,
            ITimeService timeService)
        {
            _serializer = serializer;
            _updater = updater;
            _eventIdentifierFactory = eventIdentifierFactory;
            _time = timeService;
        }

        /// <summary>
        /// Gets last global event id. It may very well be not sequental.
        /// </summary>
        /// <returns>Last global event id.</returns>
        public long GetLastEventId()
        {
            using (var context = new EventContext())
            {
                return context.Events.Max(e => (long?)e.Id) ?? -1;
            }
        }

        /// <summary>
        /// Gets last event stream version.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <returns>Last event stream version.</returns>
        public int GetLastStreamVersion(Guid streamId)
        {
            using (var context = new EventContext())
            {
                return GetLastStreamVersion(streamId, context);
            }
        }

        /// <summary>
        /// Gets all events since particular id.
        /// </summary>
        /// <param name="sinceId">Global id to get events since.</param>
        /// <param name="count">Number of events in single response (batch size).</param>
        /// <returns>List of events.</returns>
        public IEnumerable<IDomainEvent> GetAllEvents(long sinceId, int count)
        {
            using (var context = new EventContext())
            {
                return context.Events
                    .Where(e => e.Id >= sinceId
                        && e.Id < sinceId + count)
                    .AsEnumerable()
                    .Select(e => ToEvent(e))
                    .Select(e => _updater.Update(e))
                    .ToList();
            }
        }

        /// <summary>
        /// Gets events for particular event stream.
        /// </summary>
        /// <param name="streamId">Event stream id.</param>
        /// <param name="sinceVersion">Stream version to get events since.</param>
        /// <param name="count">Number of events in single response (batch size).</param>
        /// <returns>List of events.</returns>
        public IEnumerable<IDomainEvent> GetEventsForStream(
            Guid streamId, int sinceVersion, int count)
        {
            using (var context = new EventContext())
            {
                return context.Events
                    .Where(e => e.StreamId == streamId
                        && e.StreamVersion >= sinceVersion)
                    .Take(count)
                    .AsEnumerable()
                    .Select(e => ToEvent(e))
                    .Select(e => _updater.Update(e))
                    .ToList();
            }
        }

        /// <summary>
        /// Persists events to specific stream.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <param name="events">Events to be persisted.</param>
        /// <param name="expectedVersion">Expected last persisted version.</param>
        public void Save(Guid streamId, IEnumerable<IDomainEvent> events, int expectedVersion)
        {
            using (var context = new EventContext())
            {
                var last = GetLastStreamVersion(streamId, context);

                if (last != expectedVersion)
                {
                    if (last == -1)
                        throw new EventStreamDoesNotExistException(streamId);

                    throw new ExpectedVersionDoesNotMatchPersistedVersionException(
                        streamId, expectedVersion, last);
                }

                var x = FromEvent(events.First(), expectedVersion + 1 + 0);

                context.Events.AddRange(
                    events.Select((e, i) => FromEvent(e, expectedVersion + 1 + i)));

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets last event stream version using already ininitalized DbContext
        /// instance.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <param name="context">Already initialized DbContext.</param>
        /// <returns>List of events.</returns>
        private static int GetLastStreamVersion(Guid streamId, EventContext context)
        {
            return context.Events
                .Where(e => e.StreamId == streamId)
                .Max(e => (int?)e.StreamVersion) ?? -1;
        }

        /// <summary>
        /// Converts event DAO representation to domain event.
        /// </summary>
        /// <param name="dao">Data access object.</param>
        /// <returns>Domain event.</returns>
        private IDomainEvent ToEvent(EventDao dao)
        {
            var @event = (DomainEvent)_serializer.Deserialize(
                dao.EventTypeIdentifier, dao.EventData);

            @event.SetMetadata(dao.StreamId);

            return @event;
        }

        /// <summary>
        /// Converts domain event to event DAO. Sets timestamp to current UTC
        /// time at the server.
        /// </summary>
        /// <param name="event">Domain event.</param>
        /// <param name="version">Version of the domain event.</param>
        /// <returns>Data access object.</returns>
        private EventDao FromEvent(IDomainEvent @event, int version)
        {
            return new EventDao
            {
                StreamId = @event.StreamId,
                StreamVersion = version,
                EventTypeIdentifier = _eventIdentifierFactory.GetIdentifier(@event),
                EventData = _serializer.Serialize(@event),
                TimeStamp = _time.GetUtcNow()
            };
        }
    }
}
