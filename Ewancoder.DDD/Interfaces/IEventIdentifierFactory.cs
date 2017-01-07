namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Event identifier factory.
    /// </summary>
    public interface IEventIdentifierFactory
    {
        /// <summary>
        /// Obtains event identifier for given domain event type.
        /// </summary>
        /// <param name="eventType">Domain event type that needs to be
        /// identified.</param>
        /// <returns>Event identifier.</returns>
        string GetIdentifier(Type eventType);
    }

    /// <summary>
    /// Event identifier factory extensions.
    /// </summary>
    public static class EventIdentifierFactoryExtensions
    {
        /// <summary>
        /// Obtains event identifier for given domain event.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// identified.</typeparam>
        /// <param name="factory">Event identifier factory.</param>
        /// <returns>Event identifier.</returns>
        public static string GetIdentifier<TEvent>(
            this IEventIdentifierFactory factory)
            where TEvent : IDomainEvent
        {
            return factory.GetIdentifier(typeof(TEvent));
        }

        /// <summary>
        /// Obtains event identifier for given domain event.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type that needs to be
        /// identified.</typeparam>
        /// <param name="factory">Event identifier factory.</param>
        /// <param name="event">Domain event that needs to be identified.</param>
        /// <returns>Event identifier.</returns>
        public static string GetIdentifier<TEvent>(
            this IEventIdentifierFactory factory, TEvent @event)
            where TEvent : IDomainEvent
        {
            return factory.GetIdentifier<TEvent>();
        }
    }
}
