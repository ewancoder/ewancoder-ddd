namespace Ewancoder.DDD.EFEventStore.DataAccess
{
    using System.Data.Entity;

    internal sealed class SnapshotContext : DbContext
    {
        public DbSet<SnapshotDao> Snapshots { get; set; }
    }
}
