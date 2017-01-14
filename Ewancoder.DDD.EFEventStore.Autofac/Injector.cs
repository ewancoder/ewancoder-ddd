namespace Ewancoder.DDD.EFEventStore.Autofac
{
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
        public static ContainerBuilder Setup(ContainerBuilder builder)
        {
            builder.RegisterType<EventSerializer>()
                .As<IEventSerializer>();

            builder.RegisterType<SnapshotSerializer>()
                .As<ISnapshotSerializer>();

            builder.RegisterType<EventStore>()
                .As<IEventStore>();

            builder.RegisterType(typeof(SnapshotStore<>))
                .As(typeof(ISnapshotStore<>));

            return builder;
        }
    }
}
