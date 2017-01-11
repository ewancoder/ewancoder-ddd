namespace Ewancoder.DDD.EFEventStore
{
    using System;

    /// <summary>
    /// Thrown when trying to persist event stream that already has been
    /// presisted, that has some of the events already persisted or that lacks
    /// previous events in the persistence store.
    /// </summary>
    public sealed class ExpectedVersionDoesNotMatchPersistedVersionException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ExpectedVersionDoesNotMatchPersistedVersionException"/>
        /// class. </summary>
        /// <param name="streamId">Event stream id.</param>
        /// <param name="expectedVersion">Version that was expected to be persisted.</param>
        /// <param name="actualVersion">Version that failed to be persisted.</param>
        public ExpectedVersionDoesNotMatchPersistedVersionException(
            Guid streamId, int expectedVersion, int actualVersion)
        {
            StreamId = streamId;
            ExpectedVersion = expectedVersion;
            ActualVersion = actualVersion;
        }

        /// <summary>
        /// Gets event stream id.
        /// </summary>
        public Guid StreamId { get; }

        /// <summary>
        /// Gets version that was expected to be persisted.
        /// </summary>
        public int ExpectedVersion { get; }

        /// <summary>
        /// Gets version that failed to be persisted.
        /// </summary>
        public int ActualVersion { get; }
    }
}
