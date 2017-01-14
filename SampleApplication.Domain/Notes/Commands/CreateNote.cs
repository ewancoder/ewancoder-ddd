namespace SampleApplication.Domain.Notes.Commands
{
    using System;
    using Ewancoder.DDD.Interfaces;

    public sealed class CreateNote : IDomainCommand
    {
        public CreateNote(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        internal Guid Id { get; }
        internal string Name { get; }
    }
}
