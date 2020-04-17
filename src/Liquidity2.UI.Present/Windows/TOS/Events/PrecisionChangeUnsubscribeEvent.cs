using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class PrecisionChangeUnsubscribeEvent : Event
    {
        public PrecisionChangeUnsubscribeEvent(string symbol, int precision)
        {
            Symbol = symbol;
            Precision = precision;
        }

        public string Symbol { get; set; }

        public int Precision { get; set; }
    }
}
