namespace Ewancoder.DDD
{
    using System;
    using Interfaces;

    /// <summary>
    /// Event dispatcher.
    /// By default EventDispatcher doesn't throw on exception. If you need
    /// to handle exception manually, provide IEventHandlerExceptionInterceptor
    /// parameter.
    /// </summary>
    public sealed class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// Used for resolving list of event handlers for given domain event.
        /// </summary>
        private readonly IEventHandlerFactory _factory;

        /// <summary>
        /// Used for handling exception caused by any event handler. If null,
        /// exception is silently swallowed.
        /// </summary>
        private readonly IEventHandlerExceptionInterceptor _interceptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher"/> class.
        /// By default EventDispatcher doesn't throw on exception. If you need
        /// to handle exception manually, provide IEventHandlerExceptionInterceptor
        /// parameter.
        /// </summary>
        /// <param name="factory">Event handler factory.</param>
        public EventDispatcher(IEventHandlerFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        /// <param name="factory">Event handler factory.</param>
        /// <param name="interceptor">Intercepts failed event handling process.</param>
        public EventDispatcher(
            IEventHandlerFactory factory,
            IEventHandlerExceptionInterceptor interceptor)
            : this(factory)
        {
            _interceptor = interceptor;
        }

        /// <summary>
        /// Dispatches event to all appropriate event handlers.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type to be dispatched.</typeparam>
        /// <param name="event">Domain event to be dispatched.</param>
        public void Dispatch<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            foreach (var handler in _factory.Resolve<TEvent>())
            {
                try
                {
                    // Don't crash the application if event dispatch fails.
                    handler.Handle(@event);
                }
                catch (Exception e)
                {
                    _interceptor?.Intercept(@event, handler, e);
                }
            }
        }
    }
}
