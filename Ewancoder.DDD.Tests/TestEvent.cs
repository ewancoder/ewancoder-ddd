namespace Ewancoder.DDD.Tests
{
    using System;

    public class TestEvent : DomainEvent
    {
        protected TestEvent() : base(Guid.Empty) { }
        public TestEvent(Guid streamId) : base(streamId) { }
    }

    public class TestAppliedEvent : DomainEvent
    {
        public TestAppliedEvent(string prop) : base(Guid.NewGuid())
        {
            Prop = prop;
        }

        public string Prop { get; }
    }
}
