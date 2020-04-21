using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Services.SelfSelect.Events
{
    public class SelfSelectSearchEvent:Event
    {
        public SelfSelectSearchEvent(string symbol, string group)
        {
            Symbol = symbol.Replace("/", "-").ToLower();
            Group = group;
        }

        public string Symbol { get; }

        public string Group { get; }
    }
}
