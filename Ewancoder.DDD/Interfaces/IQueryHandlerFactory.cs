namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Query handler factory.
    /// </summary>
    public interface IQueryHandlerFactory
    {
        /// <summary>
        /// Obtains query handler for given domain query.
        /// </summary>
        /// <typeparam name="TQuery">Domain query type that needs to be
        /// handled.</typeparam>
        /// <typeparam name="TResult">Domain query return type.</typeparam>
        /// <returns>Query handler that handles given query.</returns>
        IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>()
            where TQuery : IDomainQuery<TResult>;
    }
}
