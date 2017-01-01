namespace Ewancoder.DDD
{
    using Interfaces;

    /// <summary>
    /// Event updater.
    /// </summary>
    public sealed class EventUpdater : IEventUpdater
    {
        /// <summary>
        /// Used to resolve event converter for given event.
        /// </summary>
        private readonly IEventConverterFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventUpdater"/> class.
        /// </summary>
        /// <param name="factory">Event converter factory.</param>
        public EventUpdater(IEventConverterFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Updates given domain event to the latest version.
        /// </summary>
        /// <param name="event">Domain event that needs to be updated to the
        /// latest version.</param>
        /// <returns>Updated event.</returns>
        public IDomainEvent Update(IDomainEvent @event)
        {
            var converter = (dynamic)EventConverterFactoryExtensions.Resolve(
                _factory, (dynamic)@event);

            while (converter != null)
            {
                @event = converter.Convert((dynamic)@event); // Converters should be public.

                converter = EventConverterFactoryExtensions.Resolve(
                    _factory, (dynamic)@event);
            }

            return @event;
        }
    }
}
