namespace Ewancoder.DDD.EFEventStore
{
    /// <summary>
    /// Serializer used to serialize and deserialize event and snapshot data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes event or snapshot data.
        /// </summary>
        /// <param name="obj">Serializable event or snapshot object.</param>
        /// <returns>Byte stream of serialized event or snapshot object.</returns>
        byte[] Serialize(object obj);

        /// <summary>
        /// Deserializes event or snapshot from binary data using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier of serialized
        /// object (metadata for deserialization).</param>
        /// <param name="data">Byte stream of serialized event or snapshot
        /// object.</param>
        /// <returns>Deserialized event or snapshot object.</returns>
        object Deserialize(string typeIdentifier, byte[] data);
    }

    /// <summary>
    /// Event serializer.
    /// </summary>
    public interface IEventSerializer : ISerializer { }

    /// <summary>
    /// Snapshot serializer.
    /// </summary>
    public interface ISnapshotSerializer : ISerializer { }
}
