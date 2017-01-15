﻿namespace SampleApplication.Domain.Notes
{
    using System;
    using Ewancoder.DDD;
    using Events;
    using Exceptions;

    internal class Note : EventStream
    {
        private bool _isArchived;
        private string _name;
        private string _body;

        /// <summary>
        /// Needed in order to reconstruct event stream by repository.
        /// </summary>
        private Note() { }

        internal Note(Guid id, string name)
        {
            ApplyChange(new NoteCreated(id, name));
        }

        internal void Archive()
        {
            if (_isArchived == true)
                throw new NoteAlreadyArchivedException(Id);

            ApplyChange(new NoteArchived(Id));
        }

        internal void Rename(string name)
        {
            if (_name != name)
                ApplyChange(new NoteNameChanged(Id, name));
        }

        internal void EditBody(string body)
        {
            if (_body != body)
                ApplyChange(new NoteBodyChanged(Id, body));
        }

        protected override void RegisterAppliers()
        {
            RegisterApplier<NoteCreated>(Apply);
            RegisterApplier<NoteArchived>(Apply);
            RegisterApplier<NoteNameChanged>(Apply);
            RegisterApplier<NoteBodyChanged>(Apply);
        }

        private void Apply(NoteCreated @event)
        {
            Id = @event.StreamId;
            _name = @event.Name;
            _body = string.Empty;
        }

        private void Apply(NoteArchived @event)
        {
            _isArchived = true;
        }

        private void Apply(NoteNameChanged @event)
        {
            _name = @event.Name;
        }

        private void Apply(NoteBodyChanged @event)
        {
            _body = @event.Body;
        }
    }
}