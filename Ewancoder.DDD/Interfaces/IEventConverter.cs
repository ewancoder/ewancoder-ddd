namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Event converter.
    /// </summary>
    /// <typeparam name="TDomainEvent">Domain event type to be converted.
    /// </typeparam>
    public interface IEventConverter<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Converts specific domain event to another domain event.
        /// </summary>
        /// <param name="event">Domain event to be converted.</param>
        /// <returns>Converted event.</returns>
        IDomainEvent Convert(TDomainEvent @event);
    }
}
