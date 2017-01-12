namespace Ewancoder.DDD.Tests
{
    using System;
    using Moq;
    using Xunit;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class FactoryExtensionsTests
    {
        [Fact]
        public void ShouldResolveCommandHandler()
        {
            var factory = new Mock<ICommandHandlerFactory>();
            ICommandHandler<TestCommand> commandHandler = new TestCommandHandler<TestCommand>();
            factory.Setup(f => f.Resolve<TestCommand>())
                .Returns(commandHandler);

            Assert.Equal(commandHandler, factory.Object.Resolve(new TestCommand()));
        }

        [Fact]
        public void ShouldResolveEventConverter()
        {
            var factory = new Mock<IEventConverterFactory>();
            IEventConverter<TestEvent> eventConverter = new TestEventConverter<TestEvent>();
            factory.Setup(f => f.Resolve<TestEvent>())
                .Returns(eventConverter);

            Assert.Equal(eventConverter, factory.Object.Resolve(new TestEvent(Guid.Empty)));
        }

        [Fact]
        public void ShouldResolveEventHandler()
        {
            var factory = new Mock<IEventHandlerFactory>();
            List<IEventHandler<TestEvent>> eventHandlers
                = new List<IEventHandler<TestEvent>>
                {
                    new TestEventHandler<TestEvent>(),
                    new TestEventHandler<TestEvent>()
                };
            factory.Setup(f => f.Resolve<TestEvent>())
                .Returns(eventHandlers);

            var resolved = factory.Object.Resolve(new TestEvent(Guid.Empty)).ToList();
            Assert.Equal(eventHandlers, resolved);
        }

        [Fact]
        public void ShouldGetEventIdentifier()
        {
            var factory = new Mock<IEventIdentifierFactory>();
            var result = "some string";
            factory.Setup(f => f.GetIdentifier(typeof(TestEvent)))
                .Returns(result);

            Assert.Equal(result, factory.Object.GetIdentifier<TestEvent>());
            Assert.Equal(result, factory.Object.GetIdentifier(new TestEvent(Guid.Empty)));
        }

        [Fact]
        public void ShouldGetEventIdentifierByInterface()
        {
            var factory = new Mock<IEventIdentifierFactory>();
            var result = "some string";
            factory.Setup(f => f.GetIdentifier(typeof(TestEvent)))
                .Returns(result);

            IDomainEvent @event = new TestEvent(Guid.Empty);
            Assert.Equal(result, factory.Object.GetIdentifier(@event));
        }

        [Fact]
        public void ShouldGetSnapshotIdentifier()
        {
            var factory = new Mock<ISnapshotIdentifierFactory>();
            var result = "some string";
            factory.Setup(f => f.GetIdentifier(typeof(TestSnapshot)))
                .Returns(result);

            Assert.Equal(result, factory.Object.GetIdentifier<TestSnapshot>());
            Assert.Equal(result, factory.Object.GetIdentifier(new TestSnapshot()));
        }

        [Fact]
        public void ShouldGetSnapshotIdentifierByInterface()
        {
            var factory = new Mock<ISnapshotIdentifierFactory>();
            var result = "some string";
            factory.Setup(f => f.GetIdentifier(typeof(TestSnapshot)))
                .Returns(result);

            IEventStreamSnapshot snapshot = new TestSnapshot();
            Assert.Equal(result, factory.Object.GetIdentifier(snapshot));
        }
    }
}
