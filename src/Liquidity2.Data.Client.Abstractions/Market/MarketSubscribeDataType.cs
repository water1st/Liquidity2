namespace Liquidity2.Data.Client.Abstractions.Market
{
    public enum MarketSubscribeDataType
    {
        /// <summary>
        /// 十档模型
        /// </summary>
        L2Item = 0,
        /// <summary>
        /// K线
        /// </summary>
        CandleItem = 1,
        /// <summary>
        /// TOS
        /// </summary>
        TOSItem = 2,
        /// <summary>
        /// Ticker
        /// </summary>
        TickerItem = 3
    }
}
