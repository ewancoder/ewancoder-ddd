namespace SampleApplication.Domain.Notes
{
    using Ewancoder.DDD.Interfaces;

    internal sealed class NoteSnapshotFactory : ISnapshotFactory<Note, INoteState>
    {
        public Note RestoreSnapshot(INoteState snapshot)
        {
            return Note.FromSnapshot(snapshot);
        }

        public INoteState TakeSnapshot(Note note)
        {
            return note.TakeSnapshot();
        }
    }
}
