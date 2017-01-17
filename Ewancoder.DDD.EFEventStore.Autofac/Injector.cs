namespace Ewancoder.DDD.EFEventStore.Autofac
{
    using global::Autofac;
    using Interfaces;
    using Services;

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

            builder.RegisterGeneric(typeof(SnapshotStore<>))
                .As(typeof(ISnapshotStore<>));

            builder.RegisterType<DefaultTimeService>()
                .As<ITimeService>();

            return builder;
        }
    }
}
