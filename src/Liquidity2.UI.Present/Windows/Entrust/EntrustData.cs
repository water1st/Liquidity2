using Liquidity2.UI.Services.DTO;

namespace Liquidity2.UI.Present.Windows.Entrust
{
    public class EntrustData
    {
        public EntrustData(string order_id, decimal price, decimal amount, string pair_symbol, string orderSide, string creatTime, ExchangeType exchange, decimal volume, decimal total)
        {
            Order_id = order_id;
            Price = price;
            Amount = amount;
            Pair_symbol = pair_symbol;
            Order_side = orderSide;
            CreateTime = creatTime;
            Exchange = exchange;
            Volume = volume;
            Total = total;
            UntradedVolume = amount - volume;
            CancelParameter = new CancelParameter(Order_id, Pair_symbol);
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_id { get; }

        /// <summary>
        /// 委托价格
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// 委托数量
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// 币对
        /// </summary>
        public string Pair_symbol { get; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string Order_side { get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType Exchange { get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume { get; }

        /// <summary>
        ///委托总额
        /// </summary>
        public decimal Total { get; }

        /// <summary>
        /// 未成交量
        /// </summary>
        public decimal UntradedVolume { get; }

        /// <summary>
        /// 撤单属性
        /// </summary>
        public CancelParameter CancelParameter { get; }
    }
}
