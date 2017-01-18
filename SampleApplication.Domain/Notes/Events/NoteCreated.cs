namespace SampleApplication.Domain.Notes.Events
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD;
    using Ewancoder.DDD.Interfaces;

    /// <summary>
    /// This was the first version of NoteCreated event.
    /// </summary>
    [DataContract]
    public sealed class NoteCreated : DomainEvent
    {
        private NoteCreated() { }
        internal NoteCreated(Guid id, string name) : base(id)
        {
            Name = name;
        }

        [DataMember(Order = 1)]
        public string Name { get; private set; }
    }

    /// <summary>
    /// Then we decided that it's prudent to create note with non-empty body.
    /// </summary>
    [DataContract]
    public sealed class NoteCreatedV2 : DomainEvent
    {
        private NoteCreatedV2() { }
        internal NoteCreatedV2(Guid id, string name, string body) : base(id)
        {
            Name = name;
            Body = body;
        }

        [DataMember(Order = 1)]
        public string Name { get; private set; }

        [DataMember(Order = 2)]
        public string Body { get; private set; }
    }

    public sealed class NoteCreatedUpdater : IEventConverter<NoteCreated>
    {
        public IDomainEvent Convert(NoteCreated @event)
        {
            return new NoteCreatedV2(@event.StreamId, @event.Name, string.Empty);
        }
    }
}
