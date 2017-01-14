namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Snapshot identifier factory.
    /// </summary>
    public interface ISnapshotIdentifierFactory
    {
        /// <summary>
        /// Obtains snapshot identifier for given event stream snapshot type.
        /// </summary>
        /// <param name="snapshotType">Event stream snapshot type that needs
        /// to be identified.</param>
        /// <returns>Event identifier.</returns>
        string GetIdentifier(Type snapshotType);

        /// <summary>
        /// Obtains snapshot type for given event stream snapshot type identifier.
        /// </summary>
        /// <param name="snapshotTypeIdentifier">Event stream snapshot type
        /// identifier.</param>
        /// <returns>Event stream snaphsot type identifier.</returns>
        Type ResolveType(string snapshotTypeIdentifier);
    }

    /// <summary>
    /// Snapshot identifier factory extensions.
    /// </summary>
    public static class SnapshotIdentifierFactoryExtensions
    {
        /// <summary>
        /// Obtains snapshot identifier for given event stream snapshot.
        /// </summary>
        /// <typeparam name="TSnapshot">Event stream snapshot type that needs
        /// to be identified.</typeparam>
        /// <param name="factory">Snapshot identifier factory.</param>
        /// <returns>Snapshot identifier.</returns>
        public static string GetIdentifier<TSnapshot>(
            this ISnapshotIdentifierFactory factory)
            where TSnapshot : IEventStreamSnapshot
        {
            return factory.GetIdentifier(typeof(TSnapshot));
        }

        /// <summary>
        /// Obtains snapshot identifier for given event stream snapshot.
        /// </summary>
        /// <typeparam name="TSnapshot">Event stream snapshot type that needs
        /// to be identified.</typeparam>
        /// <param name="factory">Snapshot identifier factory.</param>
        /// <param name="snapshot">Event stream snapshot that needs to be
        /// identified.</param>
        /// <returns>Snapshot identifier.</returns>
        public static string GetIdentifier<TSnapshot>(
            this ISnapshotIdentifierFactory factory, TSnapshot snapshot)
            where TSnapshot : IEventStreamSnapshot
        {
            return factory.GetIdentifier(snapshot.GetType());
        }
    }
}
