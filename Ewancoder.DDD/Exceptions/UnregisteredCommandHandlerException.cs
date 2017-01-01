namespace Ewancoder.DDD.Exceptions
{
    using System;
    using Interfaces;

    /// <summary>
    /// Thrown when trying to dispatch command without command handler.
    /// </summary>
    public sealed class UnregisteredCommandHandlerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnregisteredCommandHandlerException"/> class.
        /// </summary>
        /// <param name="command">Command without command handler.</param>
        public UnregisteredCommandHandlerException(IDomainCommand command)
        {
            Command = command;
        }

        /// <summary>
        /// Gets command without command handler.
        /// </summary>
        public IDomainCommand Command { get; }
    }
}
