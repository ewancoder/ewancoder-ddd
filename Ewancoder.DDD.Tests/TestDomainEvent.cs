namespace Ewancoder.DDD.Tests
{
    using System;

    public sealed class TestDomainEvent : DomainEvent
    {
        public TestDomainEvent(Guid streamId) : base(streamId)
        {
        }
    }
}
