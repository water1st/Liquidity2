using Liquidity2.Data.Client.Abstractions.Market.Events;
using Markets.Rpc.Protobuf.Request;

namespace Liquidity2.Data.Client.Api.Queries.Market
{
    public interface IMarketQueryMapper
    {
        MarketTickerQueryEvent MapToTicker(ResponseAllTickers tickers);

        MarketL2QueryEvent MapToL2QueryEvent(string symbol, ResponseOrderBook trades, int precision);

        MarketTOSQueryEvent MapToTOSQueryEvent(string symbol, ResponseTrades trades);

        MarketKLineMinQueryEvent MapToKLineQueryEvent(string symbol, ResponseCandles candles);

        MarketKLineDayQueryEvent MapToKLineDayQueryEvent(string symbol, ResponseCandles candles);

    }
}
