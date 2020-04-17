using System.Threading.Tasks;

namespace Liquidity2.Data.Client.Abstractions.Market
{
    public interface IMarketQuery
    {
        /// <summary>
        /// 查询挂单表
        /// </summary>
        /// <returns></returns>
        Task QueryOrderBook(string symbol);

        /// <summary>
        /// 查询TOS
        /// </summary>
        /// <returns></returns>
        Task QueryTrade(string symbol);

        /// <summary>
        /// 查询聚合的Ticker列表
        /// </summary>
        /// <returns></returns>
        Task QueryTickers();

        /// <summary>
        /// 根据时间查询K线
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task QueryCandlesByMinTime(string symbol, long from, long to);

        Task QueryCandlesByDayTime(string symbol, long from, long to);
    }
}
