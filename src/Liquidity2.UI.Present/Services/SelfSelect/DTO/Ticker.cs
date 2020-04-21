namespace Liquidity2.UI.Services.SelfSelect.DTO
{
    public class Ticker
    {
        public Ticker(double amount, int count, double open, double close, double low, double high, double vol, string pair)
        {
            Amount = amount;
            Count = count;
            Open = open;
            Close = close;
            Low = low;
            High = high;
            Vol = vol;
            Pair = pair;
        }

        /// <summary>
        /// 以基础币种计量的交易量
        /// </summary>
        public double Amount { get; }
        /// <summary>
        /// 交易次数
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 本阶段开盘价
        /// </summary>
        public double Open { get; }
        /// <summary>
        /// 本阶段最新价
        /// </summary>
        public double Close { get; }
        /// <summary>
        /// 本阶段最低价
        /// </summary>
        public double Low { get; }
        /// <summary>
        /// 本阶段最高价
        /// </summary>
        public double High { get; }
        /// <summary>
        /// 以报价币种计量的交易量
        /// </summary>
        public double Vol { get; }
        /// <summary>
        /// 交易对
        /// </summary>
        public string Pair { get; }
    }
}
