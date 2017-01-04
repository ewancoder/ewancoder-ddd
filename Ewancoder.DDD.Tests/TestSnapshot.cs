namespace Ewancoder.DDD.Tests
{
    using System;
    using Interfaces;

    public sealed class TestSnapshot : IEventStreamSnapshot
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
