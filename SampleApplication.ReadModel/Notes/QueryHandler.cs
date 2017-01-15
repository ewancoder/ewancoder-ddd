namespace SampleApplication.ReadModel.Notes
{
    using System.Collections.Generic;
    using Ewancoder.DDD.Interfaces;

    internal sealed class QueryHandler
        : IQueryHandler<Notes, IEnumerable<INote>>,
        IQueryHandler<NoteById, INote>
    {
        private readonly NoteStore _store;

        public QueryHandler(NoteStore store)
        {
            _store = store;
        }

        public IEnumerable<INote> Handle(Notes query)
        {
            return _store.Notes.Values;
        }

        public INote Handle(NoteById query)
        {
            return _store.Notes[query.Id];
        }
    }
}
