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

            var sut = new TestEvent(streamId);

            Assert.Equal(streamId, sut.StreamId);
        }
    }
}
