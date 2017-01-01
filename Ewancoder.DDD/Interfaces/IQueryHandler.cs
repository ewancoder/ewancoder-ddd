namespace Ewancoder.DDD.Interfaces
{
    /// <summary>
    /// Query handler.
    /// </summary>
    /// <typeparam name="TQuery">Domain query type to be handled.</typeparam>
    /// <typeparam name="TResult">Domain query return type.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IDomainQuery<TResult>
    {
        /// <summary>
        /// Handles specific domain query.
        /// </summary>
        /// <param name="query">Domain query to be handled.</param>
        /// <returns>Domain query result.</returns>
        TResult Handle(TQuery query);
    }
}
