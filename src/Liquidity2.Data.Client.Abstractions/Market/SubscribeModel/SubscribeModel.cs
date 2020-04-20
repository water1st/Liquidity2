namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class SubscribeModel
    {
        public SubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0)
        {
            Symbol = symbol;
            Type = Type;
            Precision = precision;
        }

        public int Precision { get; set; }
        public string Symbol { get; set; }
        public MarketSubscribeDataType Type { get; set; }
        public string Id { get; set; }
        public int ObserverCount { get; set; }

        public void AddObserver(int quantity = 1)
        {
            ObserverCount += quantity;
        }

        public void ReduceObserver(int quantity = 1)
        {
            ObserverCount -= quantity;
        }
    }
}
