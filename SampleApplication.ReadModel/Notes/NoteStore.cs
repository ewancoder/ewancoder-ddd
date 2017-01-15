namespace SampleApplication.ReadModel.Notes
{
    using System;
    using System.Collections.Generic;

    public sealed class NoteStore
    {
        internal IDictionary<Guid, Note> Notes { get; }
            = new Dictionary<Guid, Note>();
    }
}
