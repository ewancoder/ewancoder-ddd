namespace SampleApplication.ConsoleApp
{
    using System;
    using System.Reflection;
    using Autofac;
    using Ewancoder.DDD.Autofac;
    using Ewancoder.DDD.Interfaces;
    using Domain.Notes.Commands;
    using SampleApplication.ReadModel.Notes;
    using System.Collections.Generic;

    public class EventIdentifierFactory : IEventIdentifierFactory
    {
        public string GetIdentifier(Type eventType)
        {
            return eventType.AssemblyQualifiedName;
        }

        public Type ResolveType(string eventTypeIdentifier)
        {
            return Type.GetType(eventTypeIdentifier);
        }
    }

    public class SnapshotIdentifierFactory : ISnapshotIdentifierFactory
    {
        public string GetIdentifier(Type snapshotType)
        {
            return snapshotType.Name;
        }

        public Type ResolveType(string snapshotTypeIdentifier)
        {
            return Type.GetType(snapshotTypeIdentifier);
        }
    }

    static class Program
    {
        static void Main()
        {
            ICommandDispatcher commandDispatcher;
            IQueryDispatcher queryDispatcher;

            {
                var builder = new ContainerBuilder();

                // Setup DDD injection.
                Injector.Setup(builder, GetDomainAssemblies(), 10, 10);

                // Setup EFEventStore injection.
                Ewancoder.DDD.EFEventStore.Autofac.Injector.Setup(builder);

                // Inject identifier factories.
                builder.RegisterType<EventIdentifierFactory>()
                    .As<IEventIdentifierFactory>();

                builder.RegisterType<SnapshotIdentifierFactory>()
                    .As<ISnapshotIdentifierFactory>();

                // Setup read models.
                builder.RegisterType<NoteStore>()
                    .AsSelf().SingleInstance(); // This is in-memory cache.

                var container = builder.Build();

                commandDispatcher = container.Resolve<ICommandDispatcher>();
                queryDispatcher = container.Resolve<IQueryDispatcher>();
            }

            var noteId = Guid.NewGuid();
            var secondNoteId = Guid.NewGuid();

            {

                var createCommand = new CreateNote(noteId, "some note name");
                commandDispatcher.Dispatch(createCommand); // Creates new Note.

                var updateCommand = new UpdateNoteInformation(noteId, "another note name", "note body");
                commandDispatcher.Dispatch(updateCommand); // Updates note information.

                var archiveCommand = new ArchiveNote(noteId);
                commandDispatcher.Dispatch(archiveCommand); // Archives note.

                var createWithBodyCommand = new CreateNoteWithBody(secondNoteId, "note with body", "body");
                commandDispatcher.Dispatch(createWithBodyCommand); // Creates new Note with body.
            }

            {
                var notesQuery = new Notes();
                var notes = queryDispatcher.Dispatch<Notes, IEnumerable<INote>>(notesQuery);

                foreach (var n in notes)
                {
                    Console.WriteLine(n.Id + " - " + n.Name + " - " + n.Body);
                }

                var noteByIdQuery = new NoteById(secondNoteId);
                var note = queryDispatcher.Dispatch<NoteById, INote>(noteByIdQuery);

                Console.WriteLine("Single note with Id " + secondNoteId + " is " + note.Name);
            }

            Console.ReadLine();
        }

        private static Assembly[] GetDomainAssemblies()
        {
            return new[]
            {
                Assembly.LoadFrom("SampleApplication.Domain.dll"),
                Assembly.LoadFrom("SampleApplication.ReadModel.dll")
            };
        }
    }
}
