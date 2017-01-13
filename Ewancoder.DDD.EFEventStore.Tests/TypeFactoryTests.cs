namespace Ewancoder.DDD.EFEventStore.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class TypeFactoryTests
    {
        private class SomeEvent { }

        [Fact]
        public void ShouldResolve()
        {
            var sut = new TypeFactory(new Dictionary<string, Type>
            {
                ["string"] = typeof(string),
                ["someEvent"] = typeof(SomeEvent)
            });

            Assert.Equal(typeof(SomeEvent), sut.Resolve("someEvent"));
        }

        [Fact]
        public void ShouldThrowIfTypeIsNotRegistered()
        {
            var sut = new TypeFactory(new Dictionary<string, Type>());

            Assert.ThrowsAny<Exception>(() => sut.Resolve("someEvent"));
        }
    }

}
