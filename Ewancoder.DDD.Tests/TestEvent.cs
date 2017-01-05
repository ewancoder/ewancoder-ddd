namespace Ewancoder.DDD.Tests
{
    using System;

    public class TestEvent : DomainEvent
    {
        protected TestEvent() : base(Guid.Empty) { }
        public TestEvent(Guid streamId) : base(streamId)
        {
        }
    }
}
