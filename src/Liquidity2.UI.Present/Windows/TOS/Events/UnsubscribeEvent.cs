using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class UnsubscribeEvent:Event
    {
        public UnsubscribeEvent(string group,string symbol)
        {
            Group = group;
            Symbol = symbol;
        }

        public string Group { get; }

        public string Symbol { get; }
    }
}
