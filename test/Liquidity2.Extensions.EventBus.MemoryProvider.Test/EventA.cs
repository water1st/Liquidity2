namespace Liquidity2.Extensions.EventBus.MemoryProvider.Test
{
    public class EventA : Event
    {
        public string Name => nameof(EventA);

        public bool Called { get; set; }
    }
}
