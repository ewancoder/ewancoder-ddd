namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Event stream snapshot.
    /// </summary>
    public interface IEventStreamSnapshot
    {
        /// <summary>
        /// Gets or sets global unique identifier of the event stream. Setter
        /// may be removed in future releases.
        /// </summary>
        // HACK: Setter is used to set metadata from event store.
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets current version of the event stream. Setter may be
        /// removed in future releases.
        /// </summary>
        // HACK: Setter is used to set metadata from event store.
        int Version { get; set; }
    }
}
