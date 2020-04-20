using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.Events;
using Liquidity2.Data.Client.Abstractions.Market.SubscribeModel;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.SubscribeManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Service.Market
{
    public class MarketService : IMarketService,
        IEventHandler<MarketTosDataIncomingEvent>,
        IEventHandler<MarketL2DataIncomingEvent>,
        IEventHandler<MarketL2QueryEvent>,
        IEventHandler<MarketTOSQueryEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IMarketQuery _marketQuery;
        private readonly IMarketMapper _marketMapper;

        private readonly ISubscribeManager<L2SubscribeModel> _l2SubscribeManager;
        private readonly ISubscribeManager<TOSSubscribeModel> _tosSubscribeManager;
        private readonly ISubscribeManager<KLineSubscribeModel> _kLineSubscribeManager;
        private readonly ISubscribeManager<TickerSubscribeModel> _tickerSubscribeManager;

        //private readonly

        public MarketService(ISubscribeManagerFactory managerFactory, IEventBus eventBus, IMarketQuery marketQuery, IMarketMapper marketMapper)
        {
            _eventBus = eventBus;
            _marketQuery = marketQuery;
            _marketMapper = marketMapper;

            _l2SubscribeManager = managerFactory.Create<L2SubscribeModel>();
            _tosSubscribeManager = managerFactory.Create<TOSSubscribeModel>();
            _kLineSubscribeManager = managerFactory.Create<KLineSubscribeModel>();
            _tickerSubscribeManager = managerFactory.Create<TickerSubscribeModel>();
            Subscribe(_eventBus);
        }

        public async Task SubscribeTickerData()
        {
             await _tickerSubscribeManager.AddSubscribe(new TickerSubscribeModel(null, (MarketSubscribeDataType)DTO.MarketSubscribeDataType.TickerItem));
        }

        public async Task GetAllTickers()
        {
            await _marketQuery.QueryTickers();
        }

        public async Task GetL2Data(string symbol)
        {
            await _marketQuery.QueryOrderBook(symbol);
        }

        public async Task GetTOSData(string symbol)
        {
            await _marketQuery.QueryTrade(symbol);
        }

        public async Task<IMarketObsever> SubscribeTosData(string symbol)
        {
           var disposable = await _tosSubscribeManager.AddSubscribe(new TOSSubscribeModel(symbol, (MarketSubscribeDataType)DTO.MarketSubscribeDataType.TOSItem));
            return new MarketObserver(
                symbol,
                DTO.MarketSubscribeDataType.L2Item,
                disposable.Dispose);
        }

        public async Task<IMarketObsever> SubscribeL2Data(string symbol, int precision = 0)
        {
            var disposable = await _l2SubscribeManager.AddSubscribe(new L2SubscribeModel(symbol, (MarketSubscribeDataType)DTO.MarketSubscribeDataType.L2Item) { Precision = precision });
            return new MarketObserver(
                symbol,
                DTO.MarketSubscribeDataType.L2Item,
                disposable.Dispose,precision);
        }

        private void Subscribe(IEventBus eventBus)
        {
            eventBus.Subscribe<MarketTosDataIncomingEvent>(this);
            eventBus.Subscribe<MarketL2DataIncomingEvent>(this);
            eventBus.Subscribe<MarketL2QueryEvent>(this);
            eventBus.Subscribe<MarketTOSQueryEvent>(this);
        }

        //public async Task Unsubscribe(string symbol, DTO.MarketSubscribeDataType type, int precision = 0)
        //{
        //    switch (type)
        //    {
        //        case DTO.MarketSubscribeDataType.L2Item:
        //            await _l2SubscribeManager.RemoveSubscribe(new L2SubscribeModel(symbol, (MarketSubscribeDataType)type, precision));
        //            break;
        //        case DTO.MarketSubscribeDataType.CandleItem:
        //            await _kLineSubscribeManager.RemoveSubscribe(new KLineSubscribeModel(symbol, (MarketSubscribeDataType)type, precision));
        //            break;
        //        case DTO.MarketSubscribeDataType.TOSItem:
        //            await _tosSubscribeManager.RemoveSubscribe(new TOSSubscribeModel(symbol, (MarketSubscribeDataType)type, precision));
        //            break;
        //        case DTO.MarketSubscribeDataType.TickerItem:
        //            await _tickerSubscribeManager.RemoveSubscribe(new TickerSubscribeModel(symbol, (MarketSubscribeDataType)type, precision));
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public async Task Handle(MarketTosDataIncomingEvent @event, CancellationToken token)
        {
            var tosDataIncomingEvent = _marketMapper.MapToTosIncomgingEvent(@event);
            await _eventBus.Publish(tosDataIncomingEvent, token);
        }

        public async Task Handle(MarketL2DataIncomingEvent @event, CancellationToken token)
        {
            var L2DataIncomingEvent = _marketMapper.MapToL2IncomingEvent(@event);
            await _eventBus.Publish(L2DataIncomingEvent, token);
        }

        public async Task Handle(MarketL2QueryEvent @event, CancellationToken token)
        {
            var l2QueryEvent = _marketMapper.MapToL2QueryEvent(@event);
            await _eventBus.Publish(l2QueryEvent, token);
        }

        public async Task Handle(MarketTOSQueryEvent @event, CancellationToken token)
        {
            var tosQueryEvent = _marketMapper.MapToTOSQueryEvent(@event);
            await _eventBus.Publish(tosQueryEvent, token);
        }
    }
}
