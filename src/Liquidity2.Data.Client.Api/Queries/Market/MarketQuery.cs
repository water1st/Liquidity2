using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Extensions.EventBus;
using Markets.Rpc.Protobuf;
using Markets.Rpc.Protobuf.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Markets.Rpc.Protobuf.Request.QueryMarketService;

namespace Liquidity2.Data.Client.Api.Queries.Market
{
    class MarketQuery: IMarketQuery
    {
        private readonly QueryMarketServiceClient _queryMarketServiceClient;
        private readonly ILogger<MarketQuery> _logger;
        private readonly IEventBus _bus;
        private readonly IMarketQueryMapper _mapper;

        public MarketQuery(QueryMarketServiceClient queryMarketServiceClient, IMarketQueryMapper mapper, IEventBus bus, ILogger<MarketQuery> logger)
        {
            _queryMarketServiceClient = queryMarketServiceClient;
            _mapper = mapper;
            _bus = bus;
            _logger = logger;
        }

        public async Task QueryCandlesByDayTime(string symbol, long from, long to)
        {
            var responseCandles = await _queryMarketServiceClient.QueryCandlesByTimeAsync(new RequestCandlesByTime { From = from, To = to, Pair = symbol, Type = CandleType.Day1 });
            var kLineQueryEvent = _mapper.MapToKLineDayQueryEvent(symbol, responseCandles);
            await _bus.Publish(kLineQueryEvent, CancellationToken.None);
        }

        public async Task QueryCandlesByMinTime(string symbol, long from, long to)
        {
            var responseCandles = await _queryMarketServiceClient.QueryCandlesByTimeAsync(new RequestCandlesByTime { From = from, To = to, Pair = symbol, Type = CandleType.Minute1 });
            var kLineQueryEvent = _mapper.MapToKLineQueryEvent(symbol, responseCandles);
            await _bus.Publish(kLineQueryEvent, CancellationToken.None);
        }

        public async Task QueryOrderBook(string symbol)
        {
            try
            {
                var response = await _queryMarketServiceClient.QueryOrderBookAsync(new RequestOrderBook() { Pair = symbol });
                var orderBookEvent = _mapper.MapToL2QueryEvent(symbol, response, response.Precision);
                await _bus.Publish(orderBookEvent, CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError("查询L2错误", e);
            }
        }

        public async Task QueryTickers()
        {
            var tickers = await _queryMarketServiceClient.QueryTickersAsync(new RequestAllTickers());
            var tickerListEvent = _mapper.MapToTicker(tickers);
            await _bus.Publish(tickerListEvent, CancellationToken.None);
        }

        public async Task QueryTrade(string symbol)
        {
            try
            {
                var response = await _queryMarketServiceClient.QueryTradeAsync(new RequestTrade() { Pair = symbol });
                var tradeQueryEvent = _mapper.MapToTOSQueryEvent(symbol, response);
                await _bus.Publish(tradeQueryEvent, CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError("查询Tos错误", e);
            }
        }
    }
}
