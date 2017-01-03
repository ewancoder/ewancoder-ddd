namespace Ewancoder.DDD.Tests
{
    using Interfaces;

    public sealed class TestEventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        public TEvent Event { get; private set; }

        public void Handle(TEvent @event)
        {
            Event = @event;
        }
    }
}
