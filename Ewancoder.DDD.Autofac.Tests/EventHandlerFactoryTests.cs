namespace Ewancoder.DDD.Autofac.Tests
{
    using System.Linq;
    using global::Autofac;
    using Xunit;
    using Interfaces;

    public sealed class EventHandlerFactoryTests
    {
        [Fact]
        public void ShouldResolveEventHandlers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestEventHandler1>()
                .As<IEventHandler<TestEvent>>();
            builder.RegisterType<TestEventHandler2>()
                .As<IEventHandler<TestEvent>>();
            var container = builder.Build();

            var sut = new EventHandlerFactory(container);
            var handlers = sut.Resolve<TestEvent>().ToList();
            Assert.IsType<TestEventHandler1>(handlers[0]);
            Assert.IsType<TestEventHandler2>(handlers[1]);
        }

        [Fact]
        public void ShouldReturnEmptyCollectionIfNoRegisteredHandlers()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var sut = new EventHandlerFactory(container);
            Assert.Empty(sut.Resolve<TestEvent>());
        }
    }

    public sealed class TestEventHandler1 : IEventHandler<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
        }
    }

    public sealed class TestEventHandler2 : IEventHandler<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
        }
    }
}
