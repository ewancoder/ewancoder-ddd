namespace Ewancoder.DDD
{
    using System;
    using Interfaces;

    /// <summary>
    /// Restores state of event stream (e.g. aggregate) by applying sequence
    /// of events on it. Persists event stream (e.g. aggregate) by saving all
    /// uncommitted changes to event store.
    /// </summary>
    /// <typeparam name="TEventStream">Event stream.</typeparam>
    public sealed class Repository<TEventStream> : IRepository<TEventStream>
        where TEventStream : EventStream
    {
        /// <summary>
        /// Used to dispatch persisted events throughout the system.
        /// </summary>
        private readonly IOrderedEventDispatcher _dispatcher;

        /// <summary>
        /// Used to persists new events from event stream and to get related events.
        /// </summary>
        private readonly IEventStore _eventStore;

        /// <summary>
        /// Number of events to read per one event store query.
        /// </summary>
        private readonly int _readPageSize;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Repository{TEventStream}"/> class.
        /// </summary>
        /// <param name="dispatcher">Event dispatcher.</param>
        /// <param name="eventStore">Event store.</param>
        /// <param name="readPageSize">Number of events to read per one event
        /// store query.</param>
        public Repository(
            IOrderedEventDispatcher dispatcher,
            IEventStore eventStore,
            int readPageSize)
        {
            _dispatcher = dispatcher;
            _eventStore = eventStore;
            _readPageSize = readPageSize;
        }

        /// <summary>
        /// Gets event stream by id.
        /// </summary>
        /// <param name="id">Global unique event stream identifier.</param>
        /// <returns>Event stream.</returns>
        public TEventStream GetById(Guid id)
        {
            var lastVersion = _eventStore.EnsureStreamExistsAndGetLastVersion(id);

            var stream = (TEventStream)Activator.CreateInstance(typeof(TEventStream), true);

            _eventStore.ConstructStream(stream, id, lastVersion, _readPageSize);

            return stream;
        }

        /// <summary>
        /// Persists event stream.
        /// </summary>
        /// <param name="stream">Event stream.</param>
        public void Save(TEventStream stream)
        {
            _eventStore.SaveAndDispatch(stream, _dispatcher);
        }
    }
}
