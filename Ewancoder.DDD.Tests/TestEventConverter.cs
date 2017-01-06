namespace Ewancoder.DDD.Tests
{
    using System;
    using Interfaces;

    public sealed class TestEventConverter<TEvent> : IEventConverter<TEvent>
        where TEvent : IDomainEvent
    {
        public TEvent Event { get; private set; }

        public IDomainEvent Convert(TEvent @event)
        {
            Event = @event;

            return @event;
        }
    }
}
