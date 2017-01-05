namespace Ewancoder.DDD.Tests
{
    using System;
    using Xunit;
    using Exceptions;

    public sealed class ExceptionsTests
    {
        [Fact]
        public void EventStreamDoesNotExists()
        {
            var id = Guid.NewGuid();
            var e = new EventStreamDoesNotExistException(id);

            Assert.Equal(id, e.StreamId);
        }

        [Fact]
        public void NoEventApplierRegistered()
        {
            var eventStream = new TestEventStream();
            var @event = new TestEvent(Guid.Empty);
            var e = new NoEventApplierRegisteredException(@event, eventStream);

            Assert.Equal(@event, e.Event);
            Assert.Equal(eventStream, e.EventStream);
        }

        [Fact]
        public void UnregisteredCommandHandlerException()
        {
            var command = new TestCommand();
            var e = new UnregisteredCommandHandlerException(command);

            Assert.Equal(command, e.Command);
        }

        [Fact]
        public void UnregisteredQueryHandlerException()
        {
            var query = new TestQuery<object>();
            var e = new UnregisteredQueryHandlerException(query);

            Assert.Equal(query, e.Query);
        }
    }
}
