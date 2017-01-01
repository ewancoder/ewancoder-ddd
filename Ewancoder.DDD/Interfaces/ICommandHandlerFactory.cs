namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Command handler factory.
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Obtains command handler for given domain command.
        /// </summary>
        /// <typeparam name="TCommand">Domain command type that needs to be
        /// handled.</typeparam>
        /// <returns>Command handler that handles given command.</returns>
        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : IDomainCommand;
    }

    /// <summary>
    /// Command handler factory extensions.
    /// </summary>
    public static class CommandHandlerFactoryExtensions
    {
        /// <summary>
        /// Obtains command handler for given domain command.
        /// </summary>
        /// <typeparam name="TCommand">Domain command type that needs to be
        /// handled.</typeparam>
        /// <param name="factory">Command handler factory.</param>h
        /// <param name="command">Domain command that needs to be handled.</param>
        /// <returns>Command handler that handles given command.</returns>
        public static ICommandHandler<TCommand> Resolve<TCommand>(
            this ICommandHandlerFactory factory, TCommand command)
            where TCommand : IDomainCommand
        {
            return factory.Resolve<TCommand>();
        }
    }
}
