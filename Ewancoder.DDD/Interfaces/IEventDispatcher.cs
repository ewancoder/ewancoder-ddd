namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Event dispatcher.
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// Dispatches event to all appropriate event handlers.
        /// </summary>
        /// <typeparam name="TEvent">Domain event type to be dispatched.</typeparam>
        /// <param name="event">Domain event to be dispatched.</param>
        void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}
