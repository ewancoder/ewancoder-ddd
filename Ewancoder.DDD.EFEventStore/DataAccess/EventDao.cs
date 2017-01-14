namespace Ewancoder.DDD.EFEventStore.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("event")]
    internal sealed class EventDao
    {
        [Column("id")]
        public long Id { get; set; }

        [Index("ix_stream_id_version", Order = 1, IsUnique = true)]
        [Column("stream_id")]
        public Guid StreamId { get; set; }

        [Index("ix_stream_id_version", Order = 2, IsUnique = true)]
        [Column("stream_version")]
        public int StreamVersion { get; set; }

        [Column("event_type_identifier")]
        public string EventTypeIdentifier { get; set; }

        [Column("event_data")]
        public byte[] EventData { get; set; }

        [Index]
        [Column("timestamp")]
        public DateTime TimeStamp { get; set; }
    }
}
