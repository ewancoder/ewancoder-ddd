namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Domain event.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets global unique identifier of related event stream. In future
        /// releases, this property can become protected.
        /// </summary>
        Guid StreamId { get; }
    }
}
