using System;

namespace Liquidity2.Extensions.EventBus.MemoryProvider.Test
{
    public class EventB : Event
    {
        public Action CallCalcel { get; set; }
        public bool Called { get; set; }
    }
}
