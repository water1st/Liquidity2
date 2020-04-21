using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Services.SelfSelect.Events
{
    public class AddMarkSymbolEvent:Event
    {
        public AddMarkSymbolEvent(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}
