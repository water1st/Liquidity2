namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class TOSSubscribeModel : SubscribeModel
    {
        public TOSSubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0) : base(symbol, type, precision)
        {
        }
    }
}
