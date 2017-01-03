namespace Ewancoder.DDD.Tests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xunit;
    using Interfaces;

    public sealed class EventDispatcherTests
    {
        private readonly Mock<IEventHandlerFactory> _eventHandlerFactory;
        private readonly EventDispatcher _sut;

        private readonly Guid _streamId;
        private readonly TestEvent _testEvent;
        private readonly TestEventHandler<TestEvent> _testEventHandler1;
        private readonly TestEventHandler<TestEvent> _testEventHandler2;
        private readonly Mock<IEventHandler<TestEvent>> _errorHandler;
        private readonly Exception _exception;

        public EventDispatcherTests()
        {
            _eventHandlerFactory = new Mock<IEventHandlerFactory>();
            _sut = new EventDispatcher(_eventHandlerFactory.Object);

            _streamId = Guid.NewGuid();
            _testEvent = new TestEvent(_streamId);
            _testEventHandler1 = new TestEventHandler<TestEvent>();
            _testEventHandler2 = new TestEventHandler<TestEvent>();
            _errorHandler = new Mock<IEventHandler<TestEvent>>();
            _exception = new Exception();
            _errorHandler.Setup(h => h.Handle(_testEvent))
                .Throws(_exception);
        }

        [Fact]
        public void ShouldHandleEvent()
        {
            _eventHandlerFactory.Setup(f => f.Resolve<TestEvent>())
                .Returns(new List<IEventHandler<TestEvent>>
                {
                    _testEventHandler1,
                    _testEventHandler2
                });

            _sut.Dispatch(_testEvent);
            Assert.Equal(_testEvent, _testEventHandler1.Event);
            Assert.Equal(_testEvent, _testEventHandler2.Event);
        }

        [Fact]
        public void ShouldNotFailIfHandlerThrows()
        {
            _eventHandlerFactory.Setup(f => f.Resolve<TestEvent>())
                .Returns(new List<IEventHandler<TestEvent>>
                {
                    _testEventHandler1,
                    _errorHandler.Object,
                    _testEventHandler2
                });

            _sut.Dispatch(_testEvent);
            Assert.Equal(_testEvent, _testEventHandler1.Event);
            Assert.Equal(_testEvent, _testEventHandler2.Event);
        }

        [Fact]
        public void ShouldInterceptException()
        {
            _eventHandlerFactory.Setup(f => f.Resolve<TestEvent>())
                .Returns(new List<IEventHandler<TestEvent>>
                {
                    _errorHandler.Object
                });

            var interceptor = new TestEventHandlerExceptionInterceptor();

            var sut = new EventDispatcher(_eventHandlerFactory.Object, interceptor);
            sut.Dispatch(_testEvent);
            Assert.Equal(_testEvent, interceptor.Event);
            Assert.Equal(_errorHandler.Object, interceptor.EventHandler);
            Assert.Equal(_exception, interceptor.Exception);
        }

        private sealed class TestEventHandlerExceptionInterceptor
            : IEventHandlerExceptionInterceptor
        {
            public void Intercept<TDomainEvent, TEventHandler>(
                TDomainEvent @event, TEventHandler eventHandler, Exception exception)
                where TDomainEvent : IDomainEvent
                where TEventHandler : IEventHandler<TDomainEvent>
            {
                Event = @event;
                EventHandler = eventHandler;
                Exception = exception;
            }

            public object Event { get; private set; }
            public object EventHandler { get; private set; }
            public object Exception { get; private set; }
        }
    }
}
