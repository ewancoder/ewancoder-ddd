namespace SampleApplication.ReadModel.Notes
{
    using Ewancoder.DDD.Interfaces;
    using Domain.Notes.Events;

    internal sealed class NoteHandler
        //: IEventHandler<NoteCreated>,
        : IEventHandler<NoteCreatedV2>,
        IEventHandler<NoteArchived>,
        IEventHandler<NoteNameChanged>,
        IEventHandler<NoteBodyChanged>
    {
        private readonly NoteStore _store;

        public NoteHandler(NoteStore store)
        {
            _store = store;
        }

        /*public void Handle(NoteCreated @event)
        {
            _store.Notes.Add(@event.StreamId, new Note
            {
                Id = @event.StreamId,
                Name = @event.Name,
                Body = string.Empty
            });
        }*/

        public void Handle(NoteCreatedV2 @event)
        {
            _store.Notes.Add(@event.StreamId, new Note
            {
                Id = @event.StreamId,
                Name = @event.Name,
                Body = string.Empty
            });
        }

        public void Handle(NoteArchived @event)
        {
            _store.Notes.Remove(@event.StreamId);
        }

        public void Handle(NoteNameChanged @event)
        {
            _store.Notes[@event.StreamId].Name = @event.Name;
        }

        public void Handle(NoteBodyChanged @event)
        {
            _store.Notes[@event.StreamId].Body = @event.Body;
        }
    }
}
