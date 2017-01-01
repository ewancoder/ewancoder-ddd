namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Repository for getting and persisting event stream.
    /// </summary>
    /// <typeparam name="TEventStream">Event stream.</typeparam>
    public interface IRepository<TEventStream>
        where TEventStream : EventStream
    {
        /// <summary>
        /// Gets event stream by id.
        /// </summary>
        /// <param name="id">Global unique event stream identifier.</param>
        /// <returns>Event stream.</returns>
        TEventStream GetById(Guid id);

        /// <summary>
        /// Persists event stream.
        /// </summary>
        /// <param name="stream">Event stream.</param>
        void Save(TEventStream stream);
    }
}
