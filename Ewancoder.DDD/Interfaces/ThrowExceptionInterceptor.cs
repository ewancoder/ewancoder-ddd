namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Event handler exception interceptor that throws exception through.
    /// </summary>
    public sealed class ThrowExceptionInterceptor
        : IEventHandlerExceptionInterceptor
    {
        /// <summary>
        /// Intercepts exception thrown by handling a domain event. Throws
        /// exception through.
        /// </summary>
        /// <typeparam name="TDomainEvent">Domain event type that has been
        /// handled with exception.</typeparam>
        /// <typeparam name="TEventHandler">Event handler type that caused the exception.</typeparam>
        /// <param name="event">Domain event that has been handled with exception.</param>
        /// <param name="eventHandler">Event handler that caused the exception.</param>
        /// <param name="exception">Exception that has been thrown by the
        /// event handler while handling given event.</param>
        public void Intercept<TDomainEvent, TEventHandler>(
            TDomainEvent @event,
            TEventHandler eventHandler,
            Exception exception)
            where TDomainEvent : IDomainEvent
            where TEventHandler : IEventHandler<TDomainEvent>
        {
            throw new EventHandlerException(@event, eventHandler, exception);
        }
    }
}
