namespace SampleApplication.Domain.Notes.Events
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD;

    [DataContract]
    public sealed class NoteBodyChanged : DomainEvent
    {
        private NoteBodyChanged() { }
        internal NoteBodyChanged(Guid id, string body) : base(id)
        {
            Body = body;
        }

        [DataMember(Order = 1)]
        public string Body { get; private set; }
    }
}
