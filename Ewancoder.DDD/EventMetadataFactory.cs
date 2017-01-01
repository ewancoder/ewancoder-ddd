namespace Ewancoder.DDD
{
    using System;
    using Interfaces;

    /// <summary>
    /// Provides possibility to set metadata.
    /// </summary>
    public static class EventMetadataFactory
    {
        /// <summary>
        /// Sets metadata (namely event stream id) on the domain event.
        /// </summary>
        /// <param name="event">Domain event.</param>
        /// <param name="streamId">Event stream id.</param>
        /// <returns>Changed domain event.</returns>
        public static IDomainEvent SetMetadata(DomainEvent @event, Guid streamId)
        {
            @event.StreamId = streamId;

            return @event;
        }
    }
}
