using Liquidity2.UI.Services.DTO;

namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class HistoryEntrustData
    {
        public HistoryEntrustData(string time, string symbol, string entrustSide, decimal price, decimal amount, string orderStatus, ExchangeType exchange, double brokerage, double filledPriceAvg, string order_Id)
        {
            Time = time;
            Symbol = symbol;
            EntrustSide = entrustSide;
            Price = price;
            Amount = amount;
            OrderStatus = orderStatus;
            Exchange = exchange;
            Brokerage = brokerage.ToString("#0.########");
            FilledPriceAvg = filledPriceAvg;
            Order_Id = order_Id;
        }

        public string Time { get; }

        public string Order_Id { get; }

        public string Symbol { get; }

        public string EntrustSide { get; }

        public decimal Price { get; }

        public decimal Amount { get; }

        public string OrderStatus { get; }

        public ExchangeType Exchange { get; }

        /// <summary>
        /// 手续费
        /// </summary>
        public string Brokerage { get; set; }

        /// <summary>
        /// 成交平均价格
        /// </summary>
        public double FilledPriceAvg { get; set; }
    }
}
