namespace Ewancoder.DDD.EFEventStore
{
    using System.IO;
    using Interfaces;

    /// <summary>
    /// Serializer used to serialize and deserialize snapshot data.
    /// </summary>
    public sealed class SnapshotSerializer : ISnapshotSerializer
    {
        /// <summary>
        /// Used to resolve type from type identifier.
        /// </summary>
        private readonly ISnapshotIdentifierFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotSerializer"/> class.
        /// </summary>
        /// <param name="factory">Type factory.</param>
        public SnapshotSerializer(ISnapshotIdentifierFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Serializes snapshot data.
        /// </summary>
        /// <param name="obj">Serializable snapshot object.</param>
        /// <returns>Byte stream of serialized snapshot object.</returns>
        public byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes snapshot from binary data using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier of serialized
        /// object (metadata for deserialization).</param>
        /// <param name="data">Byte stream of serialized snapshot object.</param>
        /// <returns>Deserialized snapshot object.</returns>
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
