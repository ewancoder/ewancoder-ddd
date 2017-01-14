namespace SampleApplication.Domain.Notes.Commands
{
    using System;
    using Ewancoder.DDD.Interfaces;

    public sealed class ArchiveNote : IDomainCommand
    {
        public ArchiveNote(Guid id)
        {
            Id = id;
        }

        internal Guid Id { get; }
    }
}
