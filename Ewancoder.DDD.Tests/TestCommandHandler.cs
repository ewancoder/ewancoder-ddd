namespace Ewancoder.DDD.Tests
{
    using Interfaces;

    public sealed class TestCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IDomainCommand
    {
        public TCommand Command { get; private set; }

        public void Handle(TCommand command)
        {
            Command = command;
        }
    }
}
