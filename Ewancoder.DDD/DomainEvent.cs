namespace Ewancoder.DDD
{
    using System;
    using Interfaces;

    /// <summary>
    /// Domain event.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        protected DomainEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        public DomainEvent(Guid streamId)
        {
            StreamId = streamId;
        }

        /// <summary>
        /// Gets global unique identifier of related event stream.
        /// </summary>
        public Guid StreamId { get; internal set; }
    }
}
