namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Query dispatcher.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches query to the appropriate query handler.
        /// </summary>
        /// <typeparam name="TQuery">Domain query type to be dispatched.
        /// </typeparam>
        /// <typeparam name="TResult">Domain query return type.</typeparam>
        /// <param name="query">Domain query to be dispatched.</param>
        /// <returns>Result of the domain query.</returns>
        TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : IDomainQuery<TResult>;
    }
}
