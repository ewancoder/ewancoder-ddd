namespace Ewancoder.DDD.EFEventStore.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("snapshot")]
    internal sealed class SnapshotDao
    {
        [Column("id")]
        public long Id { get; set; }

        [Index("ix_stream_id_version", Order = 1, IsUnique = true)]
        [Column("stream_id")]
        public Guid StreamId { get; set; }

        [Index("ix_stream_id_version", Order = 2, IsUnique = true)]
        [Column("stream_version")]
        public int StreamVersion { get; set; }

        [Index]
        [MaxLength(64)]
        [Column("snapshot_type_identifier")]
        public string SnapshotTypeIdentifier { get; set; }

        [Column("snapshot_data")]
        public byte[] SnapshotData { get; set; }

        [Index]
        [Column("timestamp")]
        public DateTime TimeStamp { get; set; }
    }
}
