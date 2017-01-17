namespace Ewancoder.DDD
{
    using System;
    using System.Linq;
    using Exceptions;
    using Interfaces;

    /// <summary>
    /// Restores state of event stream (e.g. aggregate) using snapshot before
    /// applying sequence of events on it. Persists event stream (e.g. aggregate)
    /// by saving all uncommitted changes to event store. Takes snapshot in process.
    /// </summary>
    /// <typeparam name="TEventStream">Event stream.</typeparam>
    /// <typeparam name="TSnapshot">Event stream snapshot.</typeparam>
    public sealed class Repository<TEventStream, TSnapshot>
        : IRepository<TEventStream>
        where TEventStream : EventStream
        where TSnapshot : IEventStreamSnapshot
    {
        /// <summary>
        /// Used to dispatch persisted events throughout the system.
        /// </summary>
        private readonly IOrderedEventDispatcher _dispatcher;

        /// <summary>
        /// Used to persist new events from events stream and to get related events.
        /// </summary>
        private readonly IEventStore _eventStore;

        /// <summary>
        /// Number of events to read per one event store query.
        /// </summary>
        private readonly int _readPageSize;

        /// <summary>
        /// Used to take and restore event stream snapshots.
        /// </summary>
        private readonly ISnapshotFactory<TEventStream, TSnapshot> _snapshotFactory;

        /// <summary>
        /// Used to persist and get persisted event stream snapshots.
        /// </summary>
        private readonly ISnapshotStore<TSnapshot> _snapshotStore;

        /// <summary>
        /// Indicates how many events should accumulate before creating new snapshot.
        /// </summary>
        private readonly int _snapshotPeriod;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Repository{TEventStream, TSnapshot}"/> class.
        /// </summary>
        /// <param name="dispatcher">Event dispatcher.</param>
        /// <param name="eventStore">Event store.</param>
        /// <param name="readPageSize">Number of events to query in one go.</param>
        /// <param name="snapshotFactory">Event stream snapshot factory.</param>
        /// <param name="snapshotStore">Event stream snapshot store.</param>
        /// <param name="snapshotPeriod">Number of persisted events needed to
        /// make next snapshot.</param>
        public Repository(
            IOrderedEventDispatcher dispatcher,
            IEventStore eventStore,
            int readPageSize,
            ISnapshotFactory<TEventStream, TSnapshot> snapshotFactory,
            ISnapshotStore<TSnapshot> snapshotStore,
            int snapshotPeriod)
        {
            _dispatcher = dispatcher;
            _eventStore = eventStore;
            _readPageSize = readPageSize;
            _snapshotFactory = snapshotFactory;
            _snapshotStore = snapshotStore;
            _snapshotPeriod = snapshotPeriod;
        }

        /// <summary>
        /// Gets event stream by id.
        /// </summary>
        /// <param name="id">Global unique event stream identifier.</param>
        /// <returns>Event stream.</returns>
        public TEventStream GetById(Guid id)
        {
            var snapshot = _snapshotStore.GetByStreamId(id);
            var stream = Equals(snapshot, default(TSnapshot))
                ? (TEventStream)Activator.CreateInstance(typeof(TEventStream), true)
                : _snapshotFactory.RestoreSnapshot(snapshot);

            if (_eventStore != null)
            {
                var lastVersion = _eventStore.GetLastStreamVersion(id);

                if (lastVersion == -1 && Equals(snapshot, default(TSnapshot)))
                    throw new EventStreamDoesNotExistException(id);

                _eventStore.ConstructStream(stream, id, lastVersion, _readPageSize);
            }

            return stream;
        }

        /// <summary>
        /// Persists event stream.
        /// </summary>
        /// <param name="stream">Event stream.</param>
        public void Save(TEventStream stream)
        {
            if (_eventStore != null)
            {
                _eventStore.SaveAndDispatch(stream, _dispatcher);

                var snapshot = _snapshotStore.GetByStreamId(stream.StreamId);

                if ((Equals(snapshot, default(TSnapshot)) && stream.StreamVersion >= _snapshotPeriod - 1) // Stream versioning begin with 0.
                    || (!Equals(snapshot, default(TSnapshot)) && stream.StreamVersion - snapshot.Version >= _snapshotPeriod))
                {
                    _snapshotStore.Save(_snapshotFactory.TakeSnapshot(stream));
                }

                return;
            }

            _snapshotStore.Save(_snapshotFactory.TakeSnapshot(stream));
            _dispatcher.Dispatch(stream.GetUncommittedChanges().ToList());
            stream.CommitChanges();
        }
    }
}
