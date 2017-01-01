namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Event updater.
    /// </summary>
    public interface IEventUpdater
    {
        /// <summary>
        /// Updates given domain event to the latest version.
        /// </summary>
        /// <param name="event">Domain event that needs to be updated to the
        /// latest version.</param>
        /// <returns>Updated event.</returns>
        IDomainEvent Update(IDomainEvent @event);
    }
}
