﻿using Liquidity2.Data.Client.Abstractions.Market;
using Liquidity2.Data.Client.Abstractions.Market.Events;
using Liquidity2.Extensions.EventBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.Service.Market
{
    public class MarketServer : IMarketServer,
        IEventHandler<MarketTosDataIncomingEvent>,
        IEventHandler<MarketL2DataIncomingEvent>,
        IEventHandler<MarketL2QueryEvent>,
        IEventHandler<MarketTOSQueryEvent>
    {
        private readonly IMarketSubject _marketSubject;
        private readonly IEventBus _eventBus;
        private readonly IMarketQuery _marketQuery;
        private readonly IMarketMapper _marketMapper;

        public MarketServer(IMarketSubject marketSubject, IEventBus eventBus, IMarketQuery marketQuery, IMarketMapper marketMapper)
        {
            _marketSubject = marketSubject;
            _eventBus = eventBus;
            _marketQuery = marketQuery;
            _marketMapper = marketMapper;
            Subscribe(_eventBus);
        }

        public async Task SubscribeTickerData()
        {
            await _marketSubject.SubscribeAllTickers();
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

        public async Task SubscribeTosData(string symbol)
        {
            await _marketSubject.SubscribeTrades(symbol, MarketSubscribeDataType.TOSItem);
        }

        public async Task SubscribeL2Data(string symbol, int precision = 0)
        {
            await _marketSubject.SubscribeOrderBooks(symbol, MarketSubscribeDataType.L2Item, precision);
        }

        private void Subscribe(IEventBus eventBus)
        {
            eventBus.Subscribe<MarketTosDataIncomingEvent>(this);
            eventBus.Subscribe<MarketL2DataIncomingEvent>(this);
            eventBus.Subscribe<MarketL2QueryEvent>(this);
            eventBus.Subscribe<MarketTOSQueryEvent>(this);
        }

        public async Task Unsubscribe(string symbol, DTO.MarketSubscribeDataType type, int precision = 0)
        {
            await _marketSubject.Unsubscribe(symbol, (MarketSubscribeDataType)type, precision);
        }

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
            await _eventBus.Publish(l2QueryEvent,token);
        }

        public async Task Handle(MarketTOSQueryEvent @event, CancellationToken token)
        {
            var tosQueryEvent = _marketMapper.MapToTOSQueryEvent(@event);
            await _eventBus.Publish(tosQueryEvent,token);
        }
    }
}
