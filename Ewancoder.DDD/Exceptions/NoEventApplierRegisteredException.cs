namespace Ewancoder.DDD.Exceptions
{
    using System;
    using Interfaces;

    /// <summary>
    /// Thrown when event applied without proper applied being registered.
    /// </summary>
    public sealed class NoEventApplierRegisteredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NoEventApplierRegisteredException"/> class.
        /// </summary>
        /// <param name="event">Domain event without applier.</param>
        /// <param name="eventStream">Event stream without applier.</param>
        public NoEventApplierRegisteredException(
            IDomainEvent @event, EventStream eventStream)
        {
            Event = @event;
            EventStream = eventStream;
        }

        /// <summary>
        /// Gets domain event without applier.
        /// </summary>
        public IDomainEvent Event { get; }

        /// <summary>
        /// Gets event stream without applier.
        /// </summary>
        public EventStream EventStream { get; }
    }
}
