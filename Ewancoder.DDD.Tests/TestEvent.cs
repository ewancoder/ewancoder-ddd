namespace Ewancoder.DDD.Tests
{
    using System;

    public sealed class TestEvent : DomainEvent
    {
        public TestEvent(Guid streamId) : base(streamId)
        {
        }
    }
}
