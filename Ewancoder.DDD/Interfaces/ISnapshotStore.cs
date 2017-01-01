namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Snapshot store.
    /// </summary>
    /// <typeparam name="TSnapshot">Snapshot type.</typeparam>
    public interface ISnapshotStore<TSnapshot>
        where TSnapshot : IEventStreamSnapshot
    {
        /// <summary>
        /// Gets latest snapshot of single event stream.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <returns>Event stream snapshot.</returns>
        TSnapshot GetByStreamId(Guid streamId);

        /// <summary>
        /// Persists (saves) event stream snapshot.
        /// </summary>
        /// <param name="snapshot">Event stream snapshot.</param>
        void Save(TSnapshot snapshot);
    }
}
