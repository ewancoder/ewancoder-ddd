namespace Ewancoder.DDD
{
    using Exceptions;
    using Interfaces;

    /// <summary>
    /// Query dispatcher.
    /// </summary>
    public sealed class QueryDispatcher : IQueryDispatcher
    {
        /// <summary>
        /// Used for resolving query handler for given domain query.
        /// </summary>
        private readonly IQueryHandlerFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDispatcher"/> class.
        /// </summary>
        /// <param name="factory">Query handler factory.</param>
        public QueryDispatcher(IQueryHandlerFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Dispatches query to the appropriate query handler.
        /// </summary>
        /// <typeparam name="TQuery">Domain query type to be dispatched.</typeparam>
        /// <typeparam name="TResult">Result type of the domain query that needs
        /// to be dispatched.</typeparam>
        /// <param name="query">Domain query to be dispatched.</param>
        /// <returns>Domain query result.</returns>
        /// <exception cref="UnregisteredQueryHandlerException">Thrown when
        /// trying to handle domain query without query handler.</exception>
        public TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : IDomainQuery<TResult>
        {
            var handler = _factory.Resolve<TQuery, TResult>();

            if (handler == null)
                throw new UnregisteredQueryHandlerException(query);

            return handler.Handle(query);
        }
    }
}
