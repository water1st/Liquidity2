namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class KLineSubscribeModel : SubscribeModel
    {
        public KLineSubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0) : base(symbol, type, precision)
        {
        }
    }
}
