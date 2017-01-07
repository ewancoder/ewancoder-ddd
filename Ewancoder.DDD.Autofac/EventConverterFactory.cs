namespace Ewancoder.DDD.Autofac
{
    using global::Autofac;
    using Interfaces;

    /// <summary>
    /// Autofac event converter factory.
    /// </summary>
    public class EventConverterFactory : IEventConverterFactory
    {
        /// <summary>
        /// Autofact component context. Used to resolve registered event converters.
        /// </summary>
        private readonly IComponentContext _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventConverterFactory"/>
        /// class.
        /// </summary>
        /// <param name="container">Autofac component context.</param>
        public EventConverterFactory(IComponentContext container)
        {
            _container = container;
        }

        /// <summary>
        /// Obtains event converter for given domain event using autofac
        /// component context.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// converted.</typeparam>
        /// <returns>Event converter that converts given event.</returns>
        public IEventConverter<TEvent> Resolve<TEvent>()
            where TEvent : IDomainEvent
        {
            IEventConverter<TEvent> converter;

            _container.TryResolve(out converter);

            return converter;
        }
    }
}
