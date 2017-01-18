namespace SampleApplication.Domain.Notes.Handlers
{
    using Ewancoder.DDD.Interfaces;
    using Commands;

    internal sealed class NoteHandler
        //: ICommandHandler<CreateNote>,
        : ICommandHandler<CreateNoteWithBody>,
        ICommandHandler<ArchiveNote>,
        ICommandHandler<UpdateNoteInformation>
    {
        private readonly IRepository<Note> _repo;

        public NoteHandler(IRepository<Note> repo)
        {
            _repo = repo;
        }

        /*public void Handle(CreateNote command)
        {
            var note = new Note(command.Id, command.Name);

            _repo.Save(note);
        }

        public void Handle(CreateNoteWithBody command)
        {
            var note = new Note(command.Id, command.Name);
            note.EditBody(command.Body);

            _repo.Save(note);
        }*/

        public void Handle(CreateNoteWithBody command)
        {
            var note = new Note(command.Id, command.Name, command.Body);

            _repo.Save(note);
        }

        public void Handle(ArchiveNote command)
        {
            var note = _repo.GetById(command.Id);

            note.Archive();

            _repo.Save(note);
        }

        public void Handle(UpdateNoteInformation command)
        {
            var note = _repo.GetById(command.Id);

            note.Rename(command.Name);
            note.EditBody(command.Body);

            _repo.Save(note);
        }
    }
}
