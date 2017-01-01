namespace Ewancoder.DDD.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Event handler factory.
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// Obtains all event handlers for given domain event.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// handled.</typeparam>
        /// <returns>Event handler that handles given event.</returns>
        IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>()
            where TEvent : IDomainEvent;
    }

    /// <summary>
    /// Event handler factory extensions.
    /// </summary>
    public static class EventHandlerFactoryExtensions
    {
        /// <summary>
        /// Obtains all event handlers for given domain event.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// handled.</typeparam>
        /// <param name="factory">Event handler factory.</param>
        /// <param name="event">Domain event that needs to be handled.</param>
        /// <returns>Event handler that handles given event.</returns>
        public static IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>(
            this IEventHandlerFactory factory, TEvent @event)
            where TEvent : IDomainEvent
        {
            return factory.Resolve<TEvent>();
        }
    }
}
