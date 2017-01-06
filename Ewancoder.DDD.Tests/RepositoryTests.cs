namespace Ewancoder.DDD.Tests
{
    using System;
    using Moq;
    using Xunit;
    using Exceptions;
    using Interfaces;
    using System.Threading.Tasks;
    using System.Threading;

    public sealed class RepositoryTests
    {
        private readonly Mock<IEventDispatcher> _dispatcher;
        private readonly Mock<IEventStore> _eventStore;
        private readonly Repository<TestEventStream> _sut;

        public RepositoryTests()
        {
            _dispatcher = new Mock<IEventDispatcher>();
            _eventStore = new Mock<IEventStore>();
            _sut = new Repository<TestEventStream>(
                _dispatcher.Object,
                _eventStore.Object,
                2);
        }

        [Fact]
        public void ShouldThrowIfStreamNotExists()
        {
            var id = Guid.NewGuid();
            _eventStore.Setup(s => s.GetLastStreamVersion(id)).Returns(-1);

            Assert.Throws<EventStreamDoesNotExistException>(() => _sut.GetById(id));
        }

        [Fact]
        public void ShouldThrowIfNoEventsForEventStreamLoaded()
        {
            Assert.Throws<UnableToGetEventsForStreamException>(
                () => _sut.GetById(Guid.NewGuid()));
        }
    }
}
