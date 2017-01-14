namespace SampleApplication.Domain.Notes.Events
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD;

    [DataContract]
    public sealed class NoteArchived : DomainEvent
    {
        private NoteArchived() { }
        internal NoteArchived(Guid id) : base(id)
        {
        }
    }
}
