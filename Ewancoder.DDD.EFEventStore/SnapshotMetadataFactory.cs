namespace Ewancoder.DDD.EFEventStore
{
    using System;
    using Interfaces;

    /// <summary>
    /// Snapshot metadata factory. Provides possibility to set metadata.
    /// </summary>
    internal static class SnapshotMetadataFactory
    {
        /// <summary>
        /// Sets metadata (namely snapshot stream id and version) on the snapshot.
        /// </summary>
        /// <typeparam name="TSnapshot">Snapshot type.</typeparam>
        /// <param name="snapshot">Event stream snapshot.</param>
        /// <param name="id">Event stream id.</param>
        /// <param name="version">Event stream version.</param>
        /// <returns>Changed snapshot.</returns>
        public static TSnapshot SetMetadata<TSnapshot>(
            this TSnapshot snapshot, Guid id, int version)
            where TSnapshot : IEventStreamSnapshot
        {
            snapshot.Id = id;
            snapshot.Version = version;

            return snapshot;
        }
    }
}
