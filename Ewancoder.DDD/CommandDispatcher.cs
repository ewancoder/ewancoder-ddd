namespace Ewancoder.DDD
{
    using Exceptions;
    using Interfaces;

    /// <summary>
    /// Command dispatcher.
    /// </summary>
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        /// Used for resolving command handler for given domain command.
        /// </summary>
        private readonly ICommandHandlerFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        /// <param name="factory">Command handler factory.</param>
        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Dispatches command to the appropriate command handler.
        /// </summary>
        /// <typeparam name="TCommand">Domain command type to be dispatched.</typeparam>
        /// <param name="command">Domain command to be dispatched.</param>
        /// <exception cref="UnregisteredCommandHandlerException">Thrown when
        /// trying to handle domain command without command handler.</exception>
        public void Dispatch<TCommand>(TCommand command)
            where TCommand : IDomainCommand
        {
            var handler = _factory.Resolve<TCommand>();

            if (handler == null)
                throw new UnregisteredCommandHandlerException(command);

            handler.Handle(command);
        }
    }
}
