namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Event handler.
    /// </summary>
    /// <typeparam name="TEvent">Domain event type to be handled.</typeparam>
    public interface IEventHandler<in TEvent>
        where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles specific domain event.
        /// </summary>
        /// <param name="event">Domain event to be handled.</param>
        void Handle(TEvent @event);
    }
}
