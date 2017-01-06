namespace Ewancoder.DDD.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Moq;
    using Xunit;
    using Exceptions;
    using Interfaces;

    public sealed class RepositoryTests
    {
        private readonly Mock<IEventDispatcher> _dispatcher;
        private readonly Mock<IEventStore> _eventStore;
        private readonly Repository<TestEventStream> _sut;
        private readonly Repository<TestEventStream, TestSnapshot> _sutWithSnapshots;
        private readonly Mock<ISnapshotFactory<TestEventStream, TestSnapshot>> _snapshotFactory;
        private readonly Mock<ISnapshotStore<TestSnapshot>> _snapshotStore;

        public RepositoryTests()
        {
            _dispatcher = new Mock<IEventDispatcher>();
            _eventStore = new Mock<IEventStore>();
            _sut = new Repository<TestEventStream>(
                _dispatcher.Object,
                _eventStore.Object,
                2);

            _snapshotFactory = new Mock<ISnapshotFactory<TestEventStream, TestSnapshot>>();
            _snapshotStore = new Mock<ISnapshotStore<TestSnapshot>>();

            _sutWithSnapshots = new Repository<TestEventStream, TestSnapshot>(
                _dispatcher.Object,
                _eventStore.Object,
                2,
                _snapshotFactory.Object,
                _snapshotStore.Object,
                3);
        }

        [Fact]
        public void ShouldThrowIfStreamNotExists()
        {
            var id = Guid.NewGuid();
            _eventStore.Setup(s => s.GetLastStreamVersion(id)).Returns(-1);

            Assert.Throws<EventStreamDoesNotExistException>(() => _sut.GetById(id));
        }

        [Fact]
        public void ShouldThrowIfStreamNotExistsWithSnapshots()
        {
            var id = Guid.NewGuid();
            _eventStore.Setup(s => s.GetLastStreamVersion(id)).Returns(-1);

            Assert.Throws<EventStreamDoesNotExistException>(() => _sutWithSnapshots.GetById(id));
        }

        [Fact]
        public void ShouldThrowIfNoEventsForEventStreamLoaded()
        {
            Assert.Throws<UnableToGetEventsForStreamException>(
                () => _sut.GetById(Guid.NewGuid()));
        }

        [Fact]
        public void ShouldThrowIfNoEventsForEventStreamLoadedWithSnapshots()
        {
            Assert.Throws<UnableToGetEventsForStreamException>(
                () => _sutWithSnapshots.GetById(Guid.NewGuid()));
        }

        [Fact]
        public void ShouldGetById()
        {
            var id = Guid.NewGuid();
            _eventStore.Setup(s => s.GetLastStreamVersion(id)).Returns(4);
            _eventStore.Setup(s => s.GetEventsForStream(id, 0, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop1"),
                    new TestAppliedEvent("prop2")
                });
            _eventStore.Setup(s => s.GetEventsForStream(id, 2, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop3"),
                    new TestAppliedEvent("prop4")
                });
            _eventStore.Setup(s => s.GetEventsForStream(id, 4, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop5")
                });

            Assert.Equal("prop5", _sut.GetById(id).Prop);
        }

        [Fact]
        public void ShouldGetByIdWithSnapshots()
        {
            var id = Guid.NewGuid();
            _eventStore.Setup(s => s.GetLastStreamVersion(id)).Returns(4);
            _eventStore.Setup(s => s.GetEventsForStream(id, 0, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop1"),
                    new TestAppliedEvent("prop2")
                });
            _eventStore.Setup(s => s.GetEventsForStream(id, 2, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop3"),
                    new TestAppliedEvent("prop4")
                });
            _eventStore.Setup(s => s.GetEventsForStream(id, 4, 2))
                .Returns(new IDomainEvent[]
                {
                    new TestAppliedEvent("prop5")
                });

            Assert.Equal("prop5", _sutWithSnapshots.GetById(id).Prop);
        }

        [Fact]
        public void ShouldSave()
        {
            var stream = new TestEventStream();
            var e1 = new TestAppliedEvent("prop1");
            var e2 = new TestAppliedEvent("prop2");
            var e3 = new TestAppliedEvent("prop3");

            stream.TestApplyChange(e1);
            stream.TestApplyChange(e2);
            stream.TestApplyChange(e3);
            Assert.Equal("prop3", stream.Prop);

            var saved = false;
            _eventStore.Setup(s => s.Save(Guid.Empty, It.IsAny<IEnumerable<IDomainEvent>>(), -1))
                .Callback(() => saved = true);
            _sut.Save(stream);
            Assert.True(saved);
            Assert.Equal(2, stream.TestVersion);

            Thread.Sleep(1000);
            _dispatcher.Verify(d => d.Dispatch(e1));
            _dispatcher.Verify(d => d.Dispatch(e2));
            _dispatcher.Verify(d => d.Dispatch(e3));
        }

        [Fact]
        public void ShouldSaveWithSnapshots()
        {
            var stream = new TestEventStream();
            var e1 = new TestAppliedEvent("prop1");
            var e2 = new TestAppliedEvent("prop2");
            var e3 = new TestAppliedEvent("prop3");

            stream.TestApplyChange(e1);
            stream.TestApplyChange(e2);
            stream.TestApplyChange(e3);
            Assert.Equal("prop3", stream.Prop);

            var saved = false;
            _eventStore.Setup(s => s.Save(Guid.Empty, It.IsAny<IEnumerable<IDomainEvent>>(), -1))
                .Callback(() => saved = true);
            _sutWithSnapshots.Save(stream);
            Assert.True(saved);
            Assert.Equal(2, stream.TestVersion);

            Thread.Sleep(1000);
            _dispatcher.Verify(d => d.Dispatch(e1));
            _dispatcher.Verify(d => d.Dispatch(e2));
            _dispatcher.Verify(d => d.Dispatch(e3));
        }
    }
}
