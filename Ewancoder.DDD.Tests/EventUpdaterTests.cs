namespace Ewancoder.DDD.Tests
{
    using Moq;
    using Xunit;
    using Interfaces;

    public sealed class EventUpdaterTests
    {
        private readonly Mock<IEventConverterFactory> _eventConverterFactory
            = new Mock<IEventConverterFactory>();

        private readonly Mock<IEventConverter<TestEvent>> _testEventConverter;
        private readonly Mock<IEventConverter<TestUpdatedEvent1>> _testUpdatedEvent1Converter;
        private readonly TestEvent _testEvent;
        private readonly TestUpdatedEvent1 _testUpdatedEvent1;
        private readonly TestUpdatedEvent2 _testUpdatedEvent2;

        private readonly EventUpdater _sut;

        public EventUpdaterTests()
        {
            _testEvent = new TestEvent(System.Guid.NewGuid());
            _testUpdatedEvent1 = new TestUpdatedEvent1();
            _testUpdatedEvent2 = new TestUpdatedEvent2();

            _testEventConverter = new Mock<IEventConverter<TestEvent>>();
            _testEventConverter.Setup(c => c.Convert(_testEvent))
                .Returns(_testUpdatedEvent1);

            _testUpdatedEvent1Converter = new Mock<IEventConverter<TestUpdatedEvent1>>();
            _testUpdatedEvent1Converter.Setup(c => c.Convert(_testUpdatedEvent1))
                .Returns(_testUpdatedEvent2);

            _sut = new EventUpdater(_eventConverterFactory.Object);
        }

        [Fact]
        public void ShouldUpdateEvent()
        {
            _eventConverterFactory.Setup(f => f.Resolve<TestEvent>())
                .Returns(_testEventConverter.Object);

            _eventConverterFactory.Setup(f => f.Resolve<TestUpdatedEvent1>())
                .Returns(_testUpdatedEvent1Converter.Object);

            Assert.Equal(_testUpdatedEvent2, _sut.Update(_testEvent));
        }

        public class TestUpdatedEvent1 : TestEvent { }
        public class TestUpdatedEvent2 : TestEvent { }
    }
}
