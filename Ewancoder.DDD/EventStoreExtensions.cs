namespace Ewancoder.DDD
{
    using System;
    using System.Linq;
    using System.Threading;
    using Exceptions;
    using Interfaces;

    /// <summary>
    /// Common functionality for both
    /// Repository[TEventStream] and Repository[TEventStream, TSnapshot] classes.
    /// </summary>
    internal static class EventStoreExtensions
    {
        /// <summary>
        /// Ensures that event stream with given streamId exists and returns
        /// last version of the stream.
        /// </summary>
        /// <param name="eventStore">Event store.</param>
        /// <param name="streamId">Event stream identifier.</param>
        /// <returns>Last version of the stream.</returns>
        internal static int EnsureStreamExistsAndGetLastVersion(
            this IEventStore eventStore, Guid streamId)
        {
            var lastVersion = eventStore.GetLastStreamVersion(streamId);
            if (lastVersion == -1)
                throw new EventStreamDoesNotExistException(streamId);

            return lastVersion;
        }

        /// <summary>
        /// Applies persisted events to event stream. If given event stream already
        /// has Version greater than -1, loads only new events that go after
        /// event stream Version.
        /// </summary>
        /// <param name="eventStore">Event store.</param>
        /// <param name="stream">Event stream.</param>
        /// <param name="streamId">Identifier of the stream. This is needed
        /// if, for example, stream is completely blank and Id isn't set.</param>
        /// <param name="lastVersion">Last version of event stream. Passed here
        /// because it is queried earlier by EnsureStreamExistsAndGetLastVersion
        /// method.</param>
        /// <param name="readPageSize">Size of read page. The smaller it is, the
        /// more SELECT queries will be send to the database.</param>
        internal static void ConstructStream(
            this IEventStore eventStore,
            EventStream stream,
            Guid streamId,
            int lastVersion,
            int readPageSize)
        {
            var lastProcessed = stream.StreamVersion;

            while (lastProcessed < lastVersion)
            {
                var events = eventStore.GetEventsForStream(
                    streamId, lastProcessed + 1, readPageSize);

                stream.LoadFromHistory(events);

                lastProcessed = stream.StreamVersion;
            }
        }

        /// <summary>
        /// Persists new events from event stream to event store.
        /// </summary>
        /// <param name="eventStore">Event store.</param>
        /// <param name="stream">Event stream with uncommitted events.</param>
        /// <param name="dispatcher">Event dispatcher.</param>
        internal static void SaveAndDispatch(this IEventStore eventStore, EventStream stream, IEventDispatcher dispatcher)
        {
            var changes = stream.GetUncommittedChanges().ToList(); // Make copy for dispatching.
            eventStore.Save(stream.StreamId, changes, stream.StreamVersion);
            stream.CommitChanges();

            // After persistence success.
            ThreadPool.QueueUserWorkItem(_ =>
            {
                foreach (var @event in changes)
                    dispatcher.Dispatch((dynamic)@event);
            });
        }
    }
}
