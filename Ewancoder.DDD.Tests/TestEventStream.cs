namespace Ewancoder.DDD.Tests
{
    using System;
    using Interfaces;

    public sealed class TestEventStream : EventStream
    {
        public TestEventStream() { }
        public TestEventStream(IEventStreamSnapshot snapshot) : base(snapshot) { }

        public bool RegisteredAppliers { get; private set; }
        public Guid TestId => Id;
        public int TestVersion => Version;
        public string Prop { get; private set; }

        public void TestRegisterApplier<TDomainEvent>(Action<TDomainEvent> applier)
            where TDomainEvent : IDomainEvent
        {
            base.RegisterApplier(applier);
        }

        public void TestApplyChange(IDomainEvent @event)
        {
            base.ApplyChange(@event);
        }

        protected override void RegisterAppliers()
        {
            RegisteredAppliers = true;

            RegisterApplier<TestAppliedEvent>(Apply);
        }

        void Apply(TestAppliedEvent @event)
        {
            Prop = @event.Prop;
        }
    }
}
