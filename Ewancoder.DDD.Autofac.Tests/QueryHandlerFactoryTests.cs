namespace Ewancoder.DDD.Autofac.Tests
{
    using System;
    using global::Autofac;
    using Xunit;
    using Ewancoder.DDD.Interfaces;

    public sealed class QueryHandlerFactoryTests
    {
        [Fact]
        public void ShouldResolveQueryHandler()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestQueryHandler>()
                .As<IQueryHandler<TestQuery, object>>();
            var container = builder.Build();

            var sut = new QueryHandlerFactory(container);
            var handler = sut.Resolve<TestQuery, object>();
            Assert.IsType<TestQueryHandler>(handler);
        }

        [Fact]
        public void ShouldThrowIfQueryNotRegistered()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var sut = new QueryHandlerFactory(container);
            Assert.ThrowsAny<Exception>(() => sut.Resolve<TestQuery, object>());
        }
    }

    public sealed class TestQuery : IDomainQuery<object>
    {
    }

    public sealed class TestQueryHandler : IQueryHandler<TestQuery, object>
    {
        public object Handle(TestQuery query)
        {
            return query;
        }
    }
}
