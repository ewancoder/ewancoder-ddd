namespace Ewancoder.DDD.Services
{
    using System;

    /// <summary>
    /// Time service using .NET DateTime.UtcNow method.
    /// </summary>
    public sealed class DefaultTimeService : ITimeService
    {
        /// <summary>
        /// Gets current UTC time using .NET DateTime object (relative to
        /// current machine).
        /// </summary>
        /// <returns>Current machine UTC time.</returns>
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
