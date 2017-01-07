namespace Ewancoder.DDD.Autofac
{
    using global::Autofac;
    using Interfaces;

    /// <summary>
    /// Autofac query handler factory.
    /// </summary>
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        /// <summary>
        /// Autofac component context. Used to resolve registered query handlers.
        /// </summary>
        private readonly IComponentContext _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandlerFactory"/>
        /// class.
        /// </summary>
        /// <param name="container">Autofac component context.</param>
        public QueryHandlerFactory(IComponentContext container)
        {
            _container = container;
        }

        /// <summary>
        /// Obtains query handler for given domain query using autofac
        /// component context.
        /// </summary>
        /// <typeparam name="TQuery">Domain query type that needs to be handled.
        /// </typeparam>
        /// <typeparam name="TResult">Result of the domain query.</typeparam>
        /// <returns>Query handler that handles given query.</returns>
        public IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>()
            where TQuery : IDomainQuery<TResult>
        {
            return _container.Resolve<IQueryHandler<TQuery, TResult>>();
        }
    }
}
