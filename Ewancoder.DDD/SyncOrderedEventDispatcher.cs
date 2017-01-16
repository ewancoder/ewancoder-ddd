namespace Ewancoder.DDD
{
    using System.Collections.Generic;
    using System.Threading;
    using Interfaces;

    /// <summary>
    /// Synchronous ordered event dispatcher. Use it to build applications without
    /// eventual consistency.
    /// </summary>
    public sealed class SyncOrderedEventDispatcher : IOrderedEventDispatcher
    {
        /// <summary>
        /// Used to dispatch events one by one.
        /// </summary>
        private readonly IEventDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SyncOrderedEventDispatcher"/> class.
        /// </summary>
        /// <param name="dispatcher">Event dispatcher.</param>
        public SyncOrderedEventDispatcher(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Dispatches ordered events synchronously.
        /// </summary>
        /// <param name="events">Ordered events.</param>
        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
                _dispatcher.Dispatch((dynamic)@event);
        }
    }
}
