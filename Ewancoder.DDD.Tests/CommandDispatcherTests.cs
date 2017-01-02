namespace Ewancoder.DDD.Tests
{
    using Moq;
    using Xunit;
    using Exceptions;
    using Interfaces;

    public sealed class CommandDispatcherTests
    {
        private readonly Mock<ICommandHandlerFactory> _commandHandlerFactory
            = new Mock<ICommandHandlerFactory>();

        private readonly CommandDispatcher _sut;

        public CommandDispatcherTests()
        {
            _sut = new CommandDispatcher(_commandHandlerFactory.Object);
        }

        [Fact]
        public void ShouldThrowIfCommandHandlerNotFound()
        {
            Assert.Throws<UnregisteredCommandHandlerException>(
                () => _sut.Dispatch(new TestCommand()));
        }

        [Fact]
        public void ShouldHandleCommand()
        {
            var testCommand = new TestCommand();
            var testCommandHandler = new TestCommandHandler<TestCommand>();

            _commandHandlerFactory.Setup(f => f.Resolve<TestCommand>())
                .Returns(testCommandHandler);

            _sut.Dispatch(testCommand);
            Assert.Equal(testCommand, testCommandHandler.Command);
        }
    }
}
