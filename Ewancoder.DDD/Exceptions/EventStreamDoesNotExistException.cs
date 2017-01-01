namespace Ewancoder.DDD.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when trying to get non-existent event stream from the repository.
    /// </summary>
    public sealed class EventStreamDoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EventStreamDoesNotExistException"/> class.
        /// </summary>
        /// <param name="streamId">Non-existent event stream id.</param>
        public EventStreamDoesNotExistException(Guid streamId)
        {
            StreamId = streamId;
        }

        /// <summary>
        /// Gets non-existing event stream id.
        /// </summary>
        public Guid StreamId { get; }
    }
}
