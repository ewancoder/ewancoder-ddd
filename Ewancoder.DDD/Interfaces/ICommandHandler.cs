namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Command handler.
    /// </summary>
    /// <typeparam name="TCommand">Domain command type to be handled.</typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : IDomainCommand
    {
        /// <summary>
        /// Handles specific domain command.
        /// </summary>
        /// <param name="command">Domain command to be handled.</param>
        void Handle(TCommand command);
    }
}
