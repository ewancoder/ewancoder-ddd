namespace SampleApplication.ConsoleApp
{
    using System;
    using System.Reflection;
    using Autofac;
    using Ewancoder.DDD.Autofac;
    using Ewancoder.DDD.Interfaces;
    using Domain.Notes.Commands;

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
            ICommandDispatcher dispatcher;

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

                var container = builder.Build();

                dispatcher = container.Resolve<ICommandDispatcher>();
            }

            {
                var noteId = Guid.NewGuid();

                var createCommand = new CreateNote(noteId, "some note name");
                dispatcher.Dispatch(createCommand); // Creates new Note.

                var updateCommand = new UpdateNoteInformation(noteId, "another note name", "note body");
                dispatcher.Dispatch(updateCommand); // Updates note information.

                var archiveCommand = new ArchiveNote(noteId);
                dispatcher.Dispatch(archiveCommand); // Archives note.
            }
        }

        private static Assembly[] GetDomainAssemblies()
        {
            return new[]
            {
                Assembly.LoadFrom("SampleApplication.Domain.dll")
            };
        }
    }
}
