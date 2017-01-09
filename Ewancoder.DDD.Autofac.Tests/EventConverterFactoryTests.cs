namespace Ewancoder.DDD.Autofac.Tests
{
    using System;
    using global::Autofac;
    using Xunit;
    using Interfaces;

    public sealed class EventConverterFactoryTests
    {
        [Fact]
        public void ShouldResolveEventConverter()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestEventConverter>()
                .As<IEventConverter<TestEvent>>();
            var container = builder.Build();

            var sut = new EventConverterFactory(container);
            var converter = sut.Resolve<TestEvent>();
            Assert.IsType<TestEventConverter>(converter);
        }

        [Fact]
        public void ShouldReturnNullIfEventConverterNotFound()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var sut = new EventConverterFactory(container);
            Assert.Null(sut.Resolve<TestEvent>());
        }
    }

    public sealed class TestEvent : IDomainEvent
    {
        public Guid StreamId { get; }
    }

    public sealed class TestEventConverter : IEventConverter<TestEvent>
    {
        public TestEvent ConvertedEvent { get; private set; }

        public IDomainEvent Convert(TestEvent @event)
        {
            ConvertedEvent = @event;

            return @event;
        }
    }
}
