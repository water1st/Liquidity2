using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Liquidity2.Extensions.EventBus.MemoryProvider.Test
{
    public class MemoryEventBusTest
    {
        [Fact]
        public void Subscribe_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = new EventAHandler();

            var disposabler = bus.Subscribe(fakeHandler);

            Assert.NotNull(disposabler);
        }

        [Fact]
        public void Subscribe_SHOULD_THROW()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = null as IEventHandler<EventA>;

            Assert.Throws<ArgumentNullException>(() =>
            {
                bus.Subscribe(fakeHandler);
            });

        }

        [Fact]
        public void Unsubscribe_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = new EventAHandler();


            bus.Unsubscribe(fakeHandler);
        }

        [Fact]
        public void Unsubscribe_SHOULD_THROW()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = null as IEventHandler<EventA>;

            Assert.Throws<ArgumentNullException>(() =>
            {
                bus.Unsubscribe(fakeHandler);
            });
        }

        [Fact]
        public void IDisposableUnsubscribe_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = new EventAHandler();

            var disposabler = bus.Subscribe(fakeHandler);

            disposabler.Dispose();
        }

        [Fact]
        public async Task Publish_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = new EventAHandler();
            var fakeEvent = new EventA();

            bus.Subscribe(fakeHandler);
            await bus.Publish(fakeEvent, CancellationToken.None);

            Assert.True(fakeEvent.Called);
        }

        [Fact]
        public async Task Publishs_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var fakeHandler = new EventAHandler();
            var events = new Event[] { new EventA(), new EventA(), new EventA() };

            bus.Subscribe(fakeHandler);
            await bus.Publish(events, CancellationToken.None);

            foreach (var @event in events)
            {
                var e = @event as EventA;
                Assert.True(e.Called);
            }
        }

        [Fact]
        public async Task Publishs_Cancel_SHOULD_SUCCESS()
        {
            var logger = NullLogger<MemoryEventBus>.Instance;
            var bus = new MemoryEventBus(logger);
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var callback = new Action(() =>
            {
                cancellationTokenSource.Cancel();
            });
            var event1 = new EventB();
            var event2 = new EventB { CallCalcel = callback };
            var event3 = new EventB();
            var event4 = new EventB();
            var events = new Event[] { event1, event2, event3, event4 };
            var fakeHandler = new EventBHandler();

            bus.Subscribe(fakeHandler);

            await bus.Publish(events, token);

            Assert.True(event1.Called);
            Assert.True(event2.Called);
            Assert.False(event3.Called);
            Assert.False(event4.Called);
        }
    }
}
