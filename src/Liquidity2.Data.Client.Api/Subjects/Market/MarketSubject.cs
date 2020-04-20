using Grpc.Core;
using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using Liquidity2.Extensions.BackgroundJob;
using Liquidity2.Extensions.Data.Network;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.Lifecycle.Application;
using Liquidity2.Utilities;
using Markets.Rpc.Protobuf.Subscribe;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Markets.Rpc.Protobuf.Subscribe.SubMarketService;

namespace Liquidity2.Data.Client.Api.Subjects.Market
{
    public partial class MarketSubject : NetworkStageObject, IMarketSubject
    {
        private readonly IEventBus bus;
        private readonly IMarketModelMapper mapping;
        private readonly ILogger<MarketSubject> logger;
        private readonly IBackgroundJobService background;
        //调用服务的存根
        private readonly SubMarketServiceClient client;
        private readonly IReconnectService reconnectService;
        private bool tickerSubscrib = false;

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

            string baseName = GetType().FullName;
            listeneStreamJobName = $"{baseName}.ListeneStream";
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

        protected override async Task OnStop(CancellationToken token)
        {
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

        public async Task<IDisposable> Subscribe(L2SubscribeModel SubscribeModel)
        {
            await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                {
                    if (count == 5)
                    {
                        logger.LogError(e, $"订阅Lv2失败：{e.Message}");
                        SubscribeModel.ReduceObserver();
                    }
                }).ExecuteAsync(async () =>
                {
                    var response = await client.SubscribeOrderBooksAsync(new SubBookRequest() { Pair = SubscribeModel.Symbol, Token = token, Precision = (Markets.Rpc.Protobuf.Precision)SubscribeModel.Precision });
                    SubscribeModel.Id = response.Id;
                    SubscribeModel.ObserverCount = 1;
                });

            var disposabler = new Disposabler(async ()=> {
                await Unsubscribe(SubscribeModel);
            });
            return disposabler;
        }

        public async Task Unsubscribe(SubscribeModel unSubscribeModel)
        {
            await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
            {
                if (count == 5)
                {
                    unSubscribeModel.ObserverCount = 1;
                }
            }).ExecuteAsync(async () =>
            {
                await client.UnsubscribeAsync(new UnsubscribeRequest { Id = unSubscribeModel.Id, Token = token });
            });
        }

        /// <summary>
        /// TOS订阅
        /// </summary>
        /// <param name="SubscribeModel"></param>
        /// <returns></returns>
        public async Task<IDisposable> Subscribe(TOSSubscribeModel SubscribeModel)
        {
            await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
                {
                    if (count == 5)
                    {
                        logger.LogError(e, $"订阅TOS失败：{e.Message}");
                        SubscribeModel.ReduceObserver();
                    }
                }).ExecuteAsync(async () =>
                {
                    var response = await client.SubscribeTradesAsync(new SubTradeRequest() { Pair = SubscribeModel.Symbol, Token = token });
                    SubscribeModel.Id = response.Id;
                    SubscribeModel.ObserverCount = 1;
                });
            var disposabler = new Disposabler(async () => {
                await Unsubscribe(SubscribeModel);
            });
            return disposabler;
        }

        public async Task<IDisposable> Subscribe(KLineSubscribeModel SubscribeModel)
        {
            await Policy.Handle<Exception>().RetryAsync(5, (e, count) =>
            {
                if (count == 5)
                {
                    SubscribeModel.ReduceObserver();
                    logger.LogWarning($"订阅K线失败：{e.Message}");
                }
            }).ExecuteAsync(async () =>
            {
                var response = await client.SubscribeCandlesAsync(new SubCandlesRequest() { Pair = SubscribeModel.Symbol, Token = token });
                SubscribeModel.Id = response.Id;
                SubscribeModel.ObserverCount = 1;
            });
            var disposabler = new Disposabler(async () => {
                await Unsubscribe(SubscribeModel);
            });
            return disposabler;
        }

        public async Task<IDisposable> Subscribe(TickerSubscribeModel SubscribeModel)
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
            var disposabler = new Disposabler(async () => {
                await Unsubscribe(SubscribeModel);
            });
            return disposabler;
        }
    }
}
