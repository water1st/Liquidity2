using System.Threading.Tasks;

namespace Liquidity2.Data.Client.Abstractions.Market
{
    public interface IMarketSubject
    {
        /// <summary>
        /// 订阅TOS
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task SubscribeTrades(string symbol, MarketSubscribeDataType type);

        /// <summary>
        /// 订阅Lv2
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="type"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        Task SubscribeOrderBooks(string symbol, MarketSubscribeDataType type, int precision = 0);

        /// <summary>
        /// 订阅Ticker
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task SubscribeAllTickers();

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task Unsubscribe(string symbol, MarketSubscribeDataType type, int precision = 0);

        Task OnSubscribeStart();

        /// <summary>
        /// 订阅k线
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task SubscribeCandles(string symbol);
    }
}
