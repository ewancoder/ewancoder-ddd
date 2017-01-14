namespace SampleApplication.Domain.Notes.Exceptions
{
    using System;

    public sealed class NoteAlreadyArchivedException : Exception
    {
        public NoteAlreadyArchivedException(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}
