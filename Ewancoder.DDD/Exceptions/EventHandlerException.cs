namespace Ewancoder.DDD.Interfaces
{
    using System;

    /// <summary>
    /// Thrown when using ThrowExceptionInterceptor and event handler throws an
    /// exception.
    /// </summary>
    public sealed class EventHandlerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EventHandlerException"/> class.
        /// </summary>
        /// <param name="event">Domain event.</param>
        /// <param name="eventHandler">Event handler.</param>
        /// <param name="exception">Exception.</param>
        public EventHandlerException(
            object @event, object eventHandler, Exception exception)
            : base(exception.Message, exception)
        {
            Event = @event;
            EventHandler = eventHandler;
            Exception = exception;
        }

        /// <summary>
        /// Gets domain event that caused the exception.
        /// </summary>
        public object Event { get; }

        /// <summary>
        /// Gets event handler that thrown the exception.
        /// </summary>
        public object EventHandler { get; }

        /// <summary>
        /// Gets the exception that was thrown.
        /// </summary>
        public Exception Exception { get; }
    }
}
