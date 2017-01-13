namespace Ewancoder.DDD.EFEventStore.Tests
{
    using System;
    using Xunit;
    using Exceptions;

    public sealed class ExceptionsTests
    {
        [Fact]
        public void ExpectedVersionDoesNotMatchPersistedVersion()
        {
            var id = Guid.NewGuid();
            var expected = 10;
            var actual = 40;

            var e = new ExpectedVersionDoesNotMatchPersistedVersionException(
                id, expected, actual);

            Assert.Equal(id, e.StreamId);
            Assert.Equal(expected, e.ExpectedVersion);
            Assert.Equal(actual, e.ActualVersion);
        }
    }
}
