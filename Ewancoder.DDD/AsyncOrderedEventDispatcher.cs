namespace Ewancoder.DDD
{
    using System.Collections.Generic;
    using System.Threading;
    using Interfaces;

    /// <summary>
    /// Asynchronous ordered event dispatcher.
    /// </summary>
    public sealed class AsyncOrderedEventDispatcher : IOrderedEventDispatcher
    {
        /// <summary>
        /// Used to dispatch events one by one.
        /// </summary>
        private readonly IEventDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AsyncOrderedEventDispatcher"/> class.
        /// </summary>
        /// <param name="dispatcher">Event dispatcher.</param>
        public AsyncOrderedEventDispatcher(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Dispatches ordered events synchronously inside of asynchronous
        /// thread from threadpool.
        /// </summary>
        /// <param name="events">Ordered events.</param>
        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                foreach (var @event in events)
                    _dispatcher.Dispatch((dynamic)@event);
            });
        }
    }
}
