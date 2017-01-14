namespace SampleApplication.Domain.Notes.Events
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD;

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
}
