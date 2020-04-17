using Liquidity2.Extensions.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class GroupSubscribeEvent : Event
    {
        public Guid SenderId { get; }
        public string Symbol { get; }
        public string Group { get; }

        public GroupSubscribeEvent(Guid senderId, string symbol, string group)
        {
            SenderId = senderId;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Group = group ?? throw new ArgumentNullException(nameof(group));
        }
    }
}
