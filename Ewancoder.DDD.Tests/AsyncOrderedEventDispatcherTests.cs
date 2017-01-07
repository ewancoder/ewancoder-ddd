namespace Ewancoder.DDD.Tests
{
    using System;
    using System.Threading;
    using Moq;
    using Xunit;
    using Interfaces;

    public sealed class AsyncOrderedEventDispatcherTests
    {
        [Fact]
        public void ShouldDispatchEventsInOrder()
        {
            var e1 = new TestEvent(Guid.Empty);
            var e2 = new TestEvent(Guid.Empty);

            var dispatcher = new Mock<IEventDispatcher>();
            var sut = new AsyncOrderedEventDispatcher(dispatcher.Object);

            sut.Dispatch(new[]
            {
                e1, e2
            });

            Thread.Sleep(100);

            dispatcher.Verify(d => d.Dispatch(e1));
            dispatcher.Verify(d => d.Dispatch(e2));
        }
    }
}
