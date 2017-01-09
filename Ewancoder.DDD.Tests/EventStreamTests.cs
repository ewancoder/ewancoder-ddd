namespace Ewancoder.DDD.Tests
{
    using System;
    using Xunit;
    using Exceptions;

    public sealed class EventStreamTests
    {
        private readonly TestEventStream _sut;

        public EventStreamTests()
        {
            _sut = new TestEventStream();
        }

        [Fact]
        public void ShouldFailIfApplierNotRegistered()
        {
            Assert.Throws<NoEventApplierRegisteredException>(
                () => _sut.TestApplyChange(new TestEvent(Guid.Empty)));
        }

        [Fact]
        public void ShouldApplyRegisteredEvent()
        {
            TestEvent result = null;
            _sut.TestRegisterApplier<TestEvent>(e => result = e);

            var testEvent = new TestEvent(Guid.Empty);
            _sut.TestApplyChange(testEvent);
            Assert.Equal(testEvent, result);
        }

        [Fact]
        public void ShouldThrowNullReferenceIfWronglyRegisteredNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => _sut.TestRegisterApplier<TestEvent>(null));
        }

        [Fact]
        public void ShouldHaveDefaultVersionNegativeOne()
        {
            Assert.Equal(-1, _sut.TestVersion);
        }

        [Fact]
        public void ShouldNotIncrementVersionAfterEventApplied()
        {
            // Should increment it only after CommitChanges() is called.
            _sut.TestRegisterApplier<TestEvent>(e => { });
            _sut.TestApplyChange(new TestEvent(Guid.Empty));
            Assert.Equal(-1, _sut.TestVersion);
        }

        [Fact]
        public void ShouldRegisterAppliersAtConstruction()
        {
            Assert.True(_sut.RegisteredAppliers);
        }

        [Fact]
        public void ShouldRegisterAppliersAtStatefulConstruction()
        {
            Assert.True(new TestEventStream(new TestSnapshot()).RegisteredAppliers);
        }

        [Fact]
        public void ShouldSetVersionAndIdFromState()
        {
            var id = Guid.NewGuid();
            var version = 33;
            var snapshot = new TestSnapshot
            {
                Id = id,
                Version = version
            };
            var sut = new TestEventStream(snapshot);

            Assert.Equal(id, sut.TestId);
            Assert.Equal(version, sut.TestVersion);
        }

        [Fact]
        public void ShouldSetIdAndVersion()
        {
            var sut = new TestEventStream();
            var id = Guid.NewGuid();
            sut.TestId = id;
            sut.TestVersion = 123;

            Assert.Equal(id, sut.TestId);
            Assert.Equal(123, sut.TestVersion);
        }
    }
}
