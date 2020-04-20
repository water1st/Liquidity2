namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class TickerSubscribeModel : SubscribeModel
    {
        public TickerSubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0) : base(symbol, type, precision)
        {
        }
    }
}
