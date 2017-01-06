namespace Ewancoder.DDD.Tests
{
    using System;
    using Xunit;

    public sealed class EventMetadataFactoryTests
    {
        [Fact]
        public void ShouldSetMetadata()
        {
            var guid1 = new Guid("00000000-0000-0000-0000-000000000000");
            var guid2 = new Guid("11111111-1111-1111-1111-111111111111");
            var @event = new TestEvent(guid1);

            @event.SetMetadata(guid2);
            Assert.Equal(guid2, @event.StreamId);
        }
    }
}
