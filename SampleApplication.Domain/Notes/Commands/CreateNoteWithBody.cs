namespace SampleApplication.Domain.Notes.Commands
{
    using System;
    using Ewancoder.DDD.Interfaces;

    public sealed class CreateNoteWithBody : IDomainCommand
    {
        public CreateNoteWithBody(Guid id, string name, string body)
        {
            Id = id;
            Name = name;
            Body = body;
        }

        internal Guid Id { get; }
        internal string Name { get; }
        internal string Body { get; }
    }
}
