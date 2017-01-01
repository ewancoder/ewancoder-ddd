namespace Ewancoder.DDD
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Interfaces;

    /// <summary>
    /// Event stream. Any domain object (Aggregate root, Saga) should implement
    /// this interface in order to be event-driven.<para/>
    /// It should have private default constructor in order to be reconstructed
    /// by Repository.
    /// </summary>
    public abstract class EventStream
    {
        /// <summary>
        /// List of new non-persisted domain events.
        /// </summary>
        private readonly Queue<IDomainEvent> _changes;

        /// <summary>
        /// Registered event appliers.
        /// </summary>
        private readonly IDictionary<Type, Action<IDomainEvent>> _eventAppliers;

        /// <summary>
        /// Global unique identifier of the event stream.
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Version of the last persisted event.
        /// </summary>
        private int _version = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStream"/> class
        /// and registers event appliers.
        /// </summary>
        protected EventStream()
        {
            _changes = new Queue<IDomainEvent>();
            _eventAppliers = new Dictionary<Type, Action<IDomainEvent>>();

            RegisterAppliers();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStream"/> class,
        /// register event appliers and restores internal state (id, version)
        /// from the snapshot.
        /// </summary>
        /// <param name="snapshot">Snapshot to restore state from.</param>
        protected EventStream(IEventStreamSnapshot snapshot) : this()
        {
            _id = snapshot.Id;
            _version = snapshot.Version;
        }

        /// <summary>
        /// Gets global unique identifier of the event stream. Used by internal DDD
        /// library.
        /// </summary>
        internal Guid StreamId => _id;

        /// <summary>
        /// Gets event stream version. Used by internal DDD library.
        /// </summary>
        internal int StreamVersion => _version;

        /// <summary>
        /// Gets or sets global unique identifier of the event stream.
        /// For getting and setting snapshots.
        /// </summary>
        protected Guid Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Gets or sets event stream version. Used for taking and restoring snapshots.
        /// </summary>
        protected int Version
        {
            get => _version;
            set => _version = value;
        }

        /// <summary>
        /// Gets all uncommitted domain events within event stream.
        /// </summary>
        /// <returns>List of uncommitted changes.</returns>
        internal IEnumerable<IDomainEvent> GetUncommittedChanges()
            => _changes.AsEnumerable();

        /// <summary>
        /// Clears list of uncommitted domain events. Should be called after
        /// persisting event stream (transaction).
        /// </summary>
        internal void CommitChanges()
        {
            _version += _changes.Count;
            _changes.Clear();
        }

        /// <summary>
        /// Applies sequence of events in given order upon the event stream
        /// (like aggregate root). Restores previous state of an object by
        /// applying domain events.
        /// </summary>
        /// <param name="history">List of previously occurred domain events.</param>
        /// <exception cref="NoEventApplierRegisteredException">Thrown when
        /// event being applied without proper applier registration.</exception>
        internal void LoadFromHistory(IEnumerable<IDomainEvent> history)
        {
            foreach (var @event in history)
            {
                Apply(@event);
                _version += 1;
            }
        }

        /// <summary>
        /// Registers domain event appliers.
        /// </summary>
        protected abstract void RegisterAppliers();

        /// <summary>
        /// Registers single domain event applier.
        /// </summary>
        /// <typeparam name="TDomainEvent">Domain event type.</typeparam>
        /// <param name="applier">Action to be executed when given event occurs.</param>
        /// <exception cref="ArgumentNullException">Thrown when applier is null.</exception>
        protected void RegisterApplier<TDomainEvent>(Action<TDomainEvent> applier)
            where TDomainEvent : IDomainEvent
        {
            if (applier == null)
                throw new ArgumentNullException(nameof(applier));

            _eventAppliers.Add(typeof(TDomainEvent), e => applier((TDomainEvent)e));
        }

        /// <summary>
        /// Applies domain event and adds it to the list of uncommitted events.
        /// </summary>
        /// <param name="event">Domain event.</param>
        /// <exception cref="NoEventApplierRegisteredException">Thrown when
        /// event being applied without proper applier registration.</exception>
        protected void ApplyChange(IDomainEvent @event)
        {
            Apply(@event);

            _changes.Enqueue(@event);
        }

        /// <summary>
        /// Applies domain event using previously registerred applier.
        /// </summary>
        /// <param name="event">Domain event.</param>
        /// <exception cref="NoEventApplierRegisteredException">Thrown when
        /// event being applied without proper applier registration.</exception>
        private void Apply(IDomainEvent @event)
        {
            var eventType = @event.GetType();

            if (!_eventAppliers.ContainsKey(eventType))
                throw new NoEventApplierRegisteredException(@event, this);

            _eventAppliers[eventType](@event);
        }
    }
}
