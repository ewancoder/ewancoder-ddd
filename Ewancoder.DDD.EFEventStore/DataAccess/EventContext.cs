namespace Ewancoder.DDD.EFEventStore.DataAccess
{
    using System.Data.Entity;

    internal sealed class EventContext : DbContext
    {
        public DbSet<EventDao> Events { get; set; }
    }
}
