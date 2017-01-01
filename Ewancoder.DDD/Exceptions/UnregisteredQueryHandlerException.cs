namespace Ewancoder.DDD.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when trying to dispatch query without query handler.
    /// </summary>
    public sealed class UnregisteredQueryHandlerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UnregisteredQueryHandlerException"/> class.
        /// </summary>
        /// <param name="query">Query without query handler.</param>
        public UnregisteredQueryHandlerException(object query)
        {
            Query = query;
        }

        /// <summary>
        /// Gets query without command handler.
        /// </summary>
        public object Query { get; }
    }
}
