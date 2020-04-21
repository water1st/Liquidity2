using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Services.SelfSelect.Events
{
    public class RemoveMarkSymbolEvent:Event
    {
        public RemoveMarkSymbolEvent(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}
