namespace Ewancoder.DDD.EFEventStore
{
    using System;
    using System.Linq;
    using DataAccess;
    using Interfaces;

    /// <summary>
    /// Entity framework driven Snapshot Store.
    /// </summary>
    /// <typeparam name="TSnapshot">Snapshot type.</typeparam>
    public sealed class SnapshotStore<TSnapshot> : ISnapshotStore<TSnapshot>
        where TSnapshot : IEventStreamSnapshot
    {
        /// <summary>
        /// Used to serialize and deserialize snapshot data.
        /// </summary>
        private readonly ISerializer _serializer;

        /// <summary>
        /// Used to resolve snapshot type identifier.
        /// </summary>
        private readonly ISnapshotIdentifierFactory _snapshotIdentifierFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotStore{TSnapshot}"/> class.
        /// </summary>
        /// <param name="serializer">Data serializer.</param>
        /// <param name="snapshotIdentifierFactory">Snapshot identifier factory.</param>
        public SnapshotStore(
            ISerializer serializer,
            ISnapshotIdentifierFactory snapshotIdentifierFactory)
        {
            _serializer = serializer;
            _snapshotIdentifierFactory = snapshotIdentifierFactory;
        }

        /// <summary>
        /// Gets latest snapshot of single event stream.
        /// </summary>
        /// <param name="streamId">Event stream identifier.</param>
        /// <returns>Event stream snapshot.</returns>
        public TSnapshot GetByStreamId(Guid streamId)
        {
            using (var context = new SnapshotContext())
            {
                // Get latest snapshot.
                var dao = context.Snapshots
                    .Where(s => s.StreamId == streamId)
                    .OrderBy(s => s.StreamVersion)
                    .FirstOrDefault();

                if (dao == null)
                    return default(TSnapshot);

                return ToSnapshot(dao);
            }
        }

        /// <summary>
        /// Persists event stream snapshot.
        /// </summary>
        /// <param name="snapshot">Event stream snapshot.</param>
        public void Save(TSnapshot snapshot)
        {
            using (var context = new SnapshotContext())
            {
                // Persist all snapshots.
                /*context.Snapshots.RemoveRange(
                    context.Snapshots.Where(s => s.StreamId == snapshot.Id));*/

                context.Snapshots.Add(FromSnapshot(snapshot));

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Converts snapshot DAO representation to event stream snapshot.
        /// </summary>
        /// <param name="dao">Data access object.</param>
        /// <returns>Event stream snapshot.</returns>
        private TSnapshot ToSnapshot(SnapshotDao dao)
        {
            var snapshot = (TSnapshot)_serializer.Deserialize(
                dao.SnapshotTypeIdentifier, dao.SnapshotData);

            snapshot.SetMetadata(dao.StreamId, dao.StreamVersion);

            return snapshot;
        }

        /// <summary>
        /// Converts event stream snapshot to snapshot DAO. Sets timestamp to
        /// current UTC time at the server.
        /// </summary>
        /// <param name="snapshot">Event stream snapshot.</param>
        /// <returns>Data access object.</returns>
        private SnapshotDao FromSnapshot(TSnapshot snapshot)
        {
            return new SnapshotDao
            {
                StreamId = snapshot.Id,
                StreamVersion = snapshot.Version,
                SnapshotTypeIdentifier = _snapshotIdentifierFactory.GetIdentifier<TSnapshot>(),
                SnapshotData = _serializer.Serialize(snapshot),
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}
