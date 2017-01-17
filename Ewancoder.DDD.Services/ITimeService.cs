namespace Ewancoder.DDD.Services
{
    using System;

    /// <summary>
    /// Time service for acquiring precise machine-agnostic time.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// Gets current UTC time.
        /// </summary>
        /// <returns>Current UTC time.</returns>
        DateTime GetUtcNow();
    }
}
