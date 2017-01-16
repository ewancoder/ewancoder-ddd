namespace SampleApplication.Domain.Notes
{
    using System;
    using System.Runtime.Serialization;
    using Ewancoder.DDD.Interfaces;

    internal interface INoteState : IEventStreamSnapshot
    {
        bool IsArchived { get; }
        string Name { get; }
        string Body { get; }
    }

    [DataContract]
    internal sealed class NoteState : INoteState
    {
        private NoteState() { }
        public NoteState(Guid id, int version, bool isArchived, string name, string body)
        {
            Id = id;
            Version = version;
            IsArchived = isArchived;
            Name = name;
            Body = body;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }

        [DataMember(Order = 1)]
        public bool IsArchived { get; private set; }

        [DataMember(Order = 2)]
        public string Name { get; private set; }

        [DataMember(Order = 3)]
        public string Body { get; private set; }
    }
}
