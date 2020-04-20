namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class L2SubscribeModel : SubscribeModel
    {
        public L2SubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0) : base(symbol, type, precision)
        {
            Precision = precision;
        }

        public int Precision { get; set; }
    }
}
