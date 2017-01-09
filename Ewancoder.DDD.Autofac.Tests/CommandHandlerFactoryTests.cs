namespace Ewancoder.DDD.Autofac.Tests
{
    using System;
    using global::Autofac;
    using Xunit;
    using Ewancoder.DDD.Interfaces;

    public sealed class CommandHandlerFactoryTests
    {
        [Fact]
        public void ShouldResolveCommandHandler()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestCommandHandler>()
                .As<ICommandHandler<TestCommand>>();
            var container = builder.Build();

            var sut = new CommandHandlerFactory(container);
            var handler = sut.Resolve<TestCommand>();
            Assert.IsType<TestCommandHandler>(handler);
        }

        [Fact]
        public void ShouldThrowIfCommandNotRegistered()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var sut = new CommandHandlerFactory(container);
            Assert.ThrowsAny<Exception>(() => sut.Resolve<TestCommand>());
        }
    }

    public sealed class TestCommand : IDomainCommand
    {
    }

    public sealed class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public TestCommand HandledCommand { get; private set; }

        public void Handle(TestCommand command)
        {
            HandledCommand = command;
        }
    }
}
