namespace SampleApplication.Domain.Notes.Events
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD;

    [DataContract]
    public sealed class NoteNameChanged : DomainEvent
    {
        private NoteNameChanged() { }
        internal NoteNameChanged(Guid id, string name) : base(id)
        {
            Name = name;
        }

        [DataMember(Order = 1)]
        public string Name { get; private set; }
    }
}
