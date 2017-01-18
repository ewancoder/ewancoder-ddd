namespace SampleApplication.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Autofac;
    using Ewancoder.DDD;
    using Ewancoder.DDD.Autofac;
    using Ewancoder.DDD.Interfaces;
    using Domain.Notes.Commands;
    using SampleApplication.ReadModel.Notes;

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
            return snapshotType.AssemblyQualifiedName;
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
                Injector.Setup(builder, GetDomainAssemblies(), 10, 2);

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

                // Switch to synchronous event dispatching if you don't want
                // to tackle with eventual consistency.
                builder.RegisterType<SyncOrderedEventDispatcher>()
                    .As<IOrderedEventDispatcher>();

                var container = builder.Build();

                commandDispatcher = container.Resolve<ICommandDispatcher>();
                queryDispatcher = container.Resolve<IQueryDispatcher>();
            }

            var noteId = Guid.NewGuid();
            var secondNoteId = Guid.NewGuid();

            // Sends some commands to the domain.
            {
                //var createCommand = new CreateNote(noteId, "some note name");
                var createCommand = new CreateNoteWithBody(noteId, "some note name", string.Empty);
                commandDispatcher.Dispatch(createCommand); // Creates new Note.

                var updateCommand = new UpdateNoteInformation(noteId, "another note name", "note body");
                commandDispatcher.Dispatch(updateCommand); // Updates note information.

                var archiveCommand = new ArchiveNote(noteId);
                commandDispatcher.Dispatch(archiveCommand); // Archives note.

                var createWithBodyCommand = new CreateNoteWithBody(secondNoteId, "note with body", "body");
                commandDispatcher.Dispatch(createWithBodyCommand); // Creates new Note with body.
            }

            // Queries some read models from the domain.
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
