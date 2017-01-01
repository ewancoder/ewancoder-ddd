namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Command dispatcher.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatches command to the appropriate command handler.
        /// </summary>
        /// <typeparam name="TCommand">Domain command type to be dispatched.
        /// </typeparam>
        /// <param name="command">Domain command to be dispatched.</param>
        void Dispatch<TCommand>(TCommand command)
            where TCommand : IDomainCommand;
    }
}
