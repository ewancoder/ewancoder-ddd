namespace Ewancoder.DDD.Autofac
{
    using System.Collections.Generic;
    using global::Autofac;
    using Interfaces;

    /// <summary>
    /// Autofac event handler factory.
    /// </summary>
    public class EventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// Autofac component context. Used to resolve registered event handlers.
        /// </summary>
        private readonly IComponentContext _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlerFactory"/>
        /// class.
        /// </summary>
        /// <param name="container">Autofac component context.</param>
        public EventHandlerFactory(IComponentContext container)
        {
            _container = container;
        }

        /// <summary>
        /// Obtains event handler for given domain event using autofac component
        /// context.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be handled.
        /// </typeparam>
        /// <returns>Event handler that handles given event.</returns>
        public IEnumerable<IEventHandler<TEvent>> Resolve<TEvent>()
            where TEvent : IDomainEvent
        {
            return _container.Resolve<IEnumerable<IEventHandler<TEvent>>>();
        }
    }
}
