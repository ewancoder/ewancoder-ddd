namespace Ewancoder.DDD.EFEventStore
{
    using System.IO;
    using Interfaces;

    /// <summary>
    /// Serializer used to serialize and deserialize event data.
    /// </summary>
    public sealed class EventSerializer : IEventSerializer
    {
        /// <summary>
        /// Used to resolve type from type identifier.
        /// </summary>
        private readonly IEventIdentifierFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSerializer"/> class.
        /// </summary>
        /// <param name="factory">Type factory.</param>
        public EventSerializer(IEventIdentifierFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Serializes event data.
        /// </summary>
        /// <param name="obj">Serializable event object.</param>
        /// <returns>Byte stream of serialized event object.</returns>
        public byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes event from binary data using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier of serialized
        /// object (metadata for deserialization).</param>
        /// <param name="data">Byte stream of serialized event object.</param>
        /// <returns>Deserialized event object.</returns>
        public object Deserialize(string typeIdentifier, byte[] data)
        {
            var type = _factory.ResolveType(typeIdentifier);

            using (var stream = new MemoryStream(data))
            {
                return ProtoBuf.Serializer.Deserialize(type, stream);
            }
        }
    }
}
