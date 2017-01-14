namespace Ewancoder.DDD.EFEventStore.Autofac
{
    using System;
    using System.Collections.Generic;
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
        /// <returns>Autofac container builder with registered dependencies.</returns>
        public static ContainerBuilder Setup(
            ContainerBuilder builder,
            IDictionary<string, Type> knownEventTypes,
            IDictionary<string, Type> knownSnapshotTypes)
        {
            builder.RegisterType<EventStore>()
                .As<IEventStore>()
                .WithParameter("serializer", new Serializer(
                    new TypeFactory(knownEventTypes)));

            builder.RegisterGeneric(typeof(SnapshotStore<>))
                .As(typeof(ISnapshotStore<>))
                .WithParameter("serializer", new Serializer(
                    new TypeFactory(knownSnapshotTypes)));

            return builder;
        }
    }
}
