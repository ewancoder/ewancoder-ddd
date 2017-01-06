namespace Ewancoder.DDD.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when events are not loaded from event store properly.
    /// </summary>
    public sealed class UnableToGetEventsForStreamException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnableToGetEventsForStreamException"/> class.
        /// </summary>
        /// <param name="eventStream">Event stream that fails to load.</param>
        public UnableToGetEventsForStreamException(EventStream eventStream)
        {
            EventStream = eventStream;
        }

        /// <summary>
        /// Gets event stream that fails to load.
        /// </summary>
        public EventStream EventStream { get; }
    }
}
