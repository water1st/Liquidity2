using Grpc.Core;
using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Extensions.BackgroundJob;
using Liquidity2.Extensions.Data.Network;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle.Application;
using Markets.Rpc.Protobuf.Subscribe;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Markets.Rpc.Protobuf.Subscribe.SubMarketService;

namespace Liquidity2.Data.Client.Api.Subjects.Market
{
    public partial class MarketSubject: NetworkStageObject, IMarketSubject 
    {
        private readonly IEventBus bus;
        private readonly IMarketModelMapper mapping;
        private readonly ILogger<MarketSubject> logger;
        private readonly IBackgroundJobService background;
        //调用服务的存根
        private readonly SubMarketServiceClient client;
        private readonly IReconnectService reconnectService;
        private bool tickerSubscrib = false;

        //SubscribeModel键值对集合
        private readonly ConcurrentDictionary<string, SubscribeModel> subscribes;
        private string token;
        private readonly string listeneStreamJobName;

        public MarketSubject(IEventBus bus, IMarketModelMapper mapping,
            ILogger<MarketSubject> logger,
            IBackgroundJobService background,
            SubMarketServiceClient client, IReconnectService reconnectService)
        {
            this.bus = bus;
            this.mapping = mapping;
            this.logger = logger;
            this.background = background;
            this.client = client;
            this.reconnectService = reconnectService;
            subscribes = new ConcurrentDictionary<string, SubscribeModel>();

            string baseName =GetType().FullName;
            listeneStreamJobName = $"{baseName}.ListeneStream";
        }

        //订阅TOS
        public async Task SubscribeTrades(string symbol, MarketSubscribeDataType type)
        {
            var subscribe = subscribes.GetOrAdd($"[{type}][{symbol}]", name => new SubscribeModel(symbol: symbol, type: type));
            subscribe.AddObserver();

            if (subscribe.ObserverCount == 1)
            {
                await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                {
                    if (count == 5)
                    {
                        logger.LogError(e, $"订阅TOS失败：{e.Message}");
                        subscribes.TryRemove($"[{type}][{symbol}]", out _);
                        //await bus.Publish(new ConnectionFailEvent(), CancellationToken.None);
                    }
                }).ExecuteAsync(async () =>
                {
                    var response = await client.SubscribeTradesAsync(new SubTradeRequest() { Pair = symbol, Token = token });
                    subscribe.Id = response.Id;
                    subscribe.ObserverCount = 1;
                });
            }
        }

        //带精度订阅Lv2
        public async Task SubscribeOrderBooks(string symbol, MarketSubscribeDataType type, int precision = 0)
        {
            var subscribe = subscribes.GetOrAdd($"[{type}][{symbol}]", name => new SubscribeModel(symbol: symbol, type: type));
            subscribe.Precision = precision;
            subscribe.AddObserver();
            if (subscribe.ObserverCount == 1)
            {
                await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                {
                    if (count == 5)
                    {
                        subscribes.TryRemove($"[{type}][{symbol}]", out _);
                        logger.LogError(e, $"订阅Lv2失败：{e.Message}");
                    }
                }).ExecuteAsync(async () =>
                {
                    var response = await client.SubscribeOrderBooksAsync(new SubBookRequest() { Pair = symbol, Token = token, Precision = (Markets.Rpc.Protobuf.Precision)precision });
                    subscribe.Id = response.Id;
                    subscribe.ObserverCount = 1;
                });
            }
        }

        //订阅Ticker
        public async Task SubscribeAllTickers()
        {
            try
            {
                if (!tickerSubscrib)
                {
                    await client.SubscribeAllTickersAsync(new SubAllTickerRequest() { Token = token });
                    tickerSubscrib = true;
                }
            }
            catch (Exception e)
            {
                logger.LogWarning($"订阅Ticker失败：{e.Message}");
            }
        }

        //订阅k线
        public async Task SubscribeCandles(string symbol)
        {
            var subscribe = subscribes.GetOrAdd($"[{MarketSubscribeDataType.CandleItem}][{symbol}]", name => new SubscribeModel(symbol: symbol, type: MarketSubscribeDataType.CandleItem));
            subscribe.AddObserver();
            if (subscribe.ObserverCount == 1)
            {
                await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                {
                    if (count == 5)
                    {
                        subscribes.TryRemove($"[{MarketSubscribeDataType.CandleItem}][{symbol}]", out _);
                        logger.LogWarning($"订阅K线失败：{e.Message}");
                    }
                }).ExecuteAsync(async () =>
                {
                    var response = await client.SubscribeCandlesAsync(new SubCandlesRequest() { Pair = symbol, Token = token });
                    subscribe.Id = response.Id;
                    subscribe.ObserverCount = 1;
                });
            }
        }

        //取消订阅
        public async Task Unsubscribe(string symbol, MarketSubscribeDataType type, int precision = 0)
        {
            var name = $"[{type}][{symbol}]";
            if (subscribes.TryGetValue(name, out var subscribe))
            {
                subscribe.ReduceObserver();
                if (subscribe.ObserverCount == 0)
                {
                    await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                    {
                        if (count == 5)
                        {
                            subscribe.ObserverCount = 1;
                        }
                    }).ExecuteAsync(async () =>
                    {
                        await client.UnsubscribeAsync(new UnsubscribeRequest { Id = subscribe.Id, Token = token });
                        subscribes.TryRemove(name, out _);
                    });
                }
            }
        }

        public async Task OnSubscribeStart()
        {
            await reconnectService.Reconnect(CreateAsyncMessageStream, result =>
            {
                background.AddJob(listeneStreamJobName, async () =>
                {
                    await ListeneStream(result);
                });

                return Task.CompletedTask;
            }, error =>
            {
                logger.LogWarning("无法链接服务器...");
                return Task.CompletedTask;
            });
        }

        //建立订阅流
        private async Task<IAsyncStreamReader<RealtimeData>> CreateAsyncMessageStream()
        {
            try
            {
                var tokenResponse = await client.CreateTokenAsync(new TokenRequest());
                token = tokenResponse.Token;
                var stream = client.BuildConnection(new SubscribeBuilder() { Token = token });
                await stream.ResponseHeadersAsync;
                return stream.ResponseStream;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //行情推送数据
        private async Task ListeneStream(IAsyncStreamReader<RealtimeData> asyncStream)
        {
            var stream = asyncStream;
            for (bool run = true; run;)
            {
                try
                {
                    await foreach (var data in stream.ReadAllAsync())
                    {
                        Event @event = null;
                        switch (data.DataCase)
                        {
                            case RealtimeData.DataOneofCase.RawTrade:
                                @event = mapping.MapToTos(data.RawTrade, data.Pair);
                                break;
                            case RealtimeData.DataOneofCase.BookSanpshot:
                                @event = mapping.MapToL2(data.BookSanpshot, data.Pair, data.BookSanpshot.Precision);
                                break;
                            case RealtimeData.DataOneofCase.Tickers:
                                @event = mapping.MapToTickers(data.Tickers);
                                break;
                            case RealtimeData.DataOneofCase.RawCandle:
                                @event = mapping.MapToCandle(data.RawCandle, data.Pair);
                                break;
                        }
                        await bus.Publish(@event, CancellationToken.None);
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is RpcException)
                {
                    logger.LogError($"行情服务流异常：{ex.Message}");
                    tickerSubscrib = false;
                    await reconnectService.Reconnect(CreateAsyncMessageStream, async result =>
                    {
                        stream = result;
                        await ReSubscribeAll();
                    }, error =>
                    {
                        run = false;
                        return Task.CompletedTask;
                    });
                }
                catch (Exception)
                {
                }
            }
        }

        private async Task ReSubscribeAll()
        {
            await SubscribeAllTickers();
            foreach (var subscribe in subscribes.Values)
            {
                SubscribeResponse response = new SubscribeResponse();
                switch (subscribe.SubscribeType)
                {
                    case MarketSubscribeDataType.L2Item:
                        response = await client.SubscribeOrderBooksAsync(new SubBookRequest
                        {
                            Pair = subscribe.Symbol,
                            Token = token,
                            Precision = (Markets.Rpc.Protobuf.Precision)subscribe.Precision
                        });
                        break;
                    case MarketSubscribeDataType.CandleItem:
                        response = await client.SubscribeCandlesAsync(new SubCandlesRequest
                        {
                            Pair = subscribe.Symbol,
                            Token = token
                        });
                        break;
                    case MarketSubscribeDataType.TOSItem:
                        response = await client.SubscribeTradesAsync(new SubTradeRequest
                        {
                            Pair = subscribe.Symbol,
                            Token = token
                        });
                        break;
                    default:
                        break;
                }
                subscribe.Id = response.Id;
            }
        }

        private async Task UnsubscribeAll()
        {
            foreach (var item in subscribes.Values)
            {
                await client.UnsubscribeAsync(new UnsubscribeRequest { Id = item.Symbol, Token = token });
            }
            subscribes.Clear();
        }

        protected override async Task OnStop(CancellationToken token)
        {
            await UnsubscribeAll();
            background.RemoveJob(listeneStreamJobName);
        }

        protected override async Task OnStart(CancellationToken token)
        {
            await reconnectService.Reconnect(CreateAsyncMessageStream, result =>
            {
                background.AddJob(listeneStreamJobName, async () =>
                {
                    await ListeneStream(result);
                });

                return Task.CompletedTask;
            }, error =>
            {
                logger.LogWarning("无法链接服务器...");
                return Task.CompletedTask;
            });
        }
    }
}
