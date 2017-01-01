namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Snapshot factory.
    /// </summary>
    /// <typeparam name="TEventStream">Event stream type.</typeparam>
    /// <typeparam name="TSnapshot">Snapshot type.</typeparam>
    public interface ISnapshotFactory<TEventStream, TSnapshot>
        where TEventStream : EventStream
        where TSnapshot : IEventStreamSnapshot
    {
        /// <summary>
        /// Takes snapshot of current state of the event stream.
        /// </summary>
        /// <param name="eventStream">Event stream to take snapshot from.</param>
        /// <returns>Snapshot of current state of the event stream.</returns>
        TSnapshot TakeSnapshot(TEventStream eventStream);

        /// <summary>
        /// Restores event stream from previously made snapshot.
        /// </summary>
        /// <param name="snapshot">Snapshot from which event stream is restored.</param>
        /// <returns>Restored event stream.</returns>
        TEventStream RestoreSnapshot(TSnapshot snapshot);
    }
}
