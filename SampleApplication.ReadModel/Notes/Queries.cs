namespace SampleApplication.ReadModel.Notes
{
    using System;
    using System.Collections.Generic;
    using Ewancoder.DDD.Interfaces;

    public sealed class Notes : IDomainQuery<IEnumerable<INote>>
    {
    }

    public sealed class NoteById : IDomainQuery<INote>
    {
        public NoteById(Guid id)
        {
            Id = id;
        }

        internal Guid Id { get; }
    }
}
