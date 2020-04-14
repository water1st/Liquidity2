using Liquidity2.Extensions.EventBus;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class PrecisionChangeSubscribeEvent : Event
    {
        public PrecisionChangeSubscribeEvent(string symbol, int precision)
        {
            Symbol = symbol;
            Precision = precision;
        }

        public string Symbol { get; set; }

        public int Precision { get; set; }
    }
}
