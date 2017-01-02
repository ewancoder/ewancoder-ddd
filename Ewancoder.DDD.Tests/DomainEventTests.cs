namespace Ewancoder.DDD.Tests
{
    using System;
    using Xunit;

    public sealed class DomainEventTests
    {
        [Fact]
        public void ShouldSetStreamId()
        {
            var streamId = Guid.NewGuid();

            var sut = new TestDomainEvent(streamId);

            Assert.Equal(streamId, sut.StreamId);
        }
    }
}
