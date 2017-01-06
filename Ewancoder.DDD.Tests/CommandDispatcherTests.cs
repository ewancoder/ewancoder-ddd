namespace Ewancoder.DDD.Tests
{
    using Moq;
    using Xunit;
    using Exceptions;
    using Interfaces;

    public sealed class QueryDispatcherTests
    {
        private readonly Mock<IQueryHandlerFactory> _queryHandlerFactory
            = new Mock<IQueryHandlerFactory>();

        private readonly QueryDispatcher _sut;

        public QueryDispatcherTests()
        {
            _sut = new QueryDispatcher(_queryHandlerFactory.Object);
        }

        [Fact]
        public void ShouldThrowIfQueryHandlerNotFound()
        {
            Assert.Throws<UnregisteredQueryHandlerException>(
                () => _sut.Dispatch<TestQuery<object>, object>(
                    new TestQuery<object>()));
        }

        [Fact]
        public void ShouldHandleQuery()
        {
            var testQuery = new TestQuery<object>();
            object value = new object();
            var testQueryHandler = new Mock<IQueryHandler<TestQuery<object>, object>>();
            testQueryHandler.Setup(h => h.Handle(testQuery))
                .Returns(value);
            _queryHandlerFactory.Setup(f => f.Resolve<TestQuery<object>, object>())
                .Returns(testQueryHandler.Object);

            var result =_sut.Dispatch<TestQuery<object>, object>(testQuery);
            Assert.Equal(value, result);
        }
    }

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
