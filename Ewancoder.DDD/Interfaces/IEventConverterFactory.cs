namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Event converter factory.
    /// </summary>
    public interface IEventConverterFactory
    {
        /// <summary>
        /// Obtains event converter for given domain event. Returns null if not
        /// resolved.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// converted.</typeparam>
        /// <returns>Event converter that converts given command. Or null if
        /// required converter not found.</returns>
        IEventConverter<TEvent> Resolve<TEvent>()
            where TEvent : IDomainEvent;
    }

    /// <summary>
    /// Event converter factory extensions.
    /// </summary>
    public static class EventConverterFactoryExtensions
    {
        /// <summary>
        /// Obtains event converter for given domain event. Returns null if not
        /// resolved.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// converted.</typeparam>
        /// <param name="factory">Event converter factory.</param>
        /// <param name="event">Domain event that needs to be converted.</param>
        /// <returns>Event converter that converts given command. Or null if
        /// required converter not found.</returns>
        public static IEventConverter<TEvent> Resolve<TEvent>(
            this IEventConverterFactory factory, TEvent @event)
            where TEvent : IDomainEvent
        {
            return factory.Resolve<TEvent>();
        }
    }
}
