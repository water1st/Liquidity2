namespace Liquidity2.Data.Client.Abstractions.DTO
{
    public class KLineItem
    {
        public KLineItem(decimal close, decimal high, decimal low, decimal open, decimal turnover, decimal volume, long unixTimeStamp)
        {
            Close = close;
            High = high;
            Low = low;
            Open = open;
            Turnover = turnover;
            Volume = volume;
            UnixTimeStamp = unixTimeStamp;
        }

        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal Close { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal Open { get; set; }
        /// <summary>
        /// 成交额
        /// </summary>
        public decimal Turnover { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// K线时间戳 seconds
        /// </summary>
        public long UnixTimeStamp { get; set; }
    }
}
