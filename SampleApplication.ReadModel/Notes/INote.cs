namespace SampleApplication.ReadModel.Notes
{
    using System;

    public interface INote
    {
        Guid Id { get; }
        string Name { get; }
        string Body { get; }
    }

    internal sealed class Note : INote
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}
