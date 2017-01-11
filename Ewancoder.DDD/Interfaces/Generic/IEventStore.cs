namespace Ewancoder.DDD.Interfaces.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generic event store.
    /// </summary>
    public interface IEventStore<TEvent>
    {
        /// <summary>
        /// Gets last global event Id.
        /// </summary>
        /// <returns>Last global event id.</returns>
        long GetLastEventId();

        /// <summary>
        /// Gets last event stream version.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <returns>Last event stream version.</returns>
        int GetLastStreamVersion(Guid streamId);

        /// <summary>
        /// Gets all events since particular id.
        /// </summary>
        /// <param name="sinceId">Global id to get events since.</param>
        /// <param name="count">Number of events in single response (batch size).</param>
        /// <returns>List of events.</returns>
        IEnumerable<TEvent> GetAllEvents(long sinceId, int count);

        /// <summary>
        /// Gets events for particular event stream.
        /// </summary>
        /// <param name="streamId">Event stream id.</param>
        /// <param name="sinceVersion">Stream version id to get events since.</param>
        /// <param name="count">Number of events in single response (batch size).</param>
        /// <returns>List of events.</returns>
        IEnumerable<TEvent> GetEventsForStream(Guid streamId, int sinceVersion, int count);

        /// <summary>
        /// Persists events to specific stream.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <param name="events">Events to be persisted.</param>
        /// <param name="expectedVersion">Expected last persisted version.</param>
        void Save(Guid streamId, IEnumerable<TEvent> events, int expectedVersion);
    }
}
