namespace Ewancoder.DDD.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Ordered event dispatcher.
    /// </summary>
    public interface IOrderedEventDispatcher
    {
        /// <summary>
        /// Dispatches event collection in order.
        /// </summary>
        /// <param name="events">Ordered domain events.</param>
        void Dispatch(IEnumerable<IDomainEvent> events);
    }
}
