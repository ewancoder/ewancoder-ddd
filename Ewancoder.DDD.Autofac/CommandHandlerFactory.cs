namespace Ewancoder.DDD.Autofac
{
    using global::Autofac;
    using Interfaces;

    /// <summary>
    /// Autofac command handler factory.
    /// </summary>
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        /// <summary>
        /// Autofac component context. Used to resolve registered command handlers.
        /// </summary>
        private readonly IComponentContext _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandlerFactory"/>
        /// class.
        /// </summary>
        /// <param name="container">Autofac component context.</param>
        public CommandHandlerFactory(IComponentContext container)
        {
            _container = container;
        }

        /// <summary>
        /// Obtains command handler for given domain command using autofac
        /// component context.
        /// </summary>
        /// <typeparam name="TCommand">Domain command type that needs to be
        /// handled.</typeparam>
        /// <returns>Command handler that handles given command.</returns>
        public ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : IDomainCommand
        {
            return _container.Resolve<ICommandHandler<TCommand>>();
        }
    }
}
