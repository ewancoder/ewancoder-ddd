namespace Ewancoder.DDD.EFEventStore
{
    using System.IO;

    /// <summary>
    /// Serializer used to serialize and deserialize event and snapshot data.
    /// </summary>
    public sealed class Serializer : ISerializer
    {
        /// <summary>
        /// Used to resolve type from type identifier.
        /// </summary>
        private readonly ITypeFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Serializer"/> class.
        /// </summary>
        /// <param name="factory">Type factory.</param>
        public Serializer(ITypeFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Serializes event or snapshot data.
        /// </summary>
        /// <param name="obj">Serializable event or snapshot object.</param>
        /// <returns>Byte stream of serialized event or snapshot object.</returns>
        public byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes event or snapshot from binary data using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier of serialized
        /// object (metadata for deserialization).</param>
        /// <param name="data">Byte stream of serialized event or snapshot
        /// object.</param>
        /// <returns>Deserialized event or snapshot object.</returns>
        public object Deserialize(string typeIdentifier, byte[] data)
        {
            var type = _factory.Resolve(typeIdentifier);

            using (var stream = new MemoryStream(data))
            {
                return ProtoBuf.Serializer.Deserialize(type, stream);
            }
        }
    }
}
