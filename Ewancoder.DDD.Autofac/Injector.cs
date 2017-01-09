namespace Ewancoder.DDD.Autofac
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Autofac;
    using Interfaces;

    /// <summary>
    /// Injects all dependencies into autofac container.
    /// </summary>
    public static class Injector
    {
        /// <summary>
        /// Registers all dependencies in container builder.
        /// </summary>
        /// <param name="builder">Autofac container builder.</param>
        /// <param name="assemblies">Assemblies to read types from.</param>
        /// <param name="readPageSize">Event read page size indicates how many
        /// events will be read from event store in one query.</param>
        /// <param name="snapshotPeriod">Snapshot period indicates how often a
        /// snapshot will be made (each n events).</param>
        /// <returns>Autofac container builder with registered dependencies.</returns>
        public static ContainerBuilder Setup(
            ContainerBuilder builder,
            IEnumerable<Assembly> assemblies,
            int readPageSize,
            int snapshotPeriod)
        {
            foreach (var asm in assemblies)
            {
                // Handlers.
                builder.RegisterAssemblyTypes(asm)
                    .AsClosedTypesOf(typeof(ICommandHandler<>));
                builder.RegisterAssemblyTypes(asm)
                    .AsClosedTypesOf(typeof(IEventHandler<>));
                builder.RegisterAssemblyTypes(asm)
                    .AsClosedTypesOf(typeof(IQueryHandler<,>));
                builder.RegisterAssemblyTypes(asm)
                    .AsClosedTypesOf(typeof(IEventConverter<>));
                builder.RegisterAssemblyTypes(asm)
                    .AsClosedTypesOf(typeof(ISnapshotFactory<,>));
            }

            // Factories.
            builder.RegisterType<CommandHandlerFactory>()
                .As<ICommandHandlerFactory>();

            builder.RegisterType<EventHandlerFactory>()
                .As<IEventHandlerFactory>();

            builder.RegisterType<QueryHandlerFactory>()
                .As<IQueryHandlerFactory>();

            builder.RegisterType<EventConverterFactory>()
                .As<IEventConverterFactory>();

            // Dispatchers.
            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .SingleInstance();

            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .SingleInstance();

            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>();

            // Default ordered event dispatcher.
            builder.RegisterType<AsyncOrderedEventDispatcher>()
                .As<IOrderedEventDispatcher>();

            // Event updater.
            builder.RegisterType<EventUpdater>()
                .As<IEventUpdater>();

            // Repository.
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .WithParameter("readPageSize", readPageSize);

            // Repository with snapshots support.
            var al = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClosedTypeOf(typeof(ISnapshotFactory<,>)))
                .Select(t => t.GetInterfaces().SingleOrDefault(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ISnapshotFactory<,>)))
                .Select(t => t.GetGenericArguments())
                .Select(a => new Type[]
                {
                    typeof(Repository<,>).MakeGenericType(a),
                    typeof(IRepository<>).MakeGenericType(a[0])
                });

            foreach (var type in al)
            {
                builder.RegisterType(type[0])
                    .As(type[1])
                    .WithParameter("readPageSize", readPageSize)
                    .WithParameter("snapshotPeriod", snapshotPeriod);
            }

            return builder;
        }
    }
}
