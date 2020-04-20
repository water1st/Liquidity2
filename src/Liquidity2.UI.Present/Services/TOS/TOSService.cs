using Liquidity2.Extensions.EventBus;
using Liquidity2.Service.Market;
using Liquidity2.Service.Market.DTO;
using Liquidity2.Service.Market.Events;
using Liquidity2.UI.Services.TOS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public partial class TOSService : ITOSService,
        IEventHandler<MarketL2DataIncomingToUIEvent>,
        IEventHandler<MarketTOSDataIncomingToUIEvent>,
        IEventHandler<MarketL2QueryToUIEvent>,
        IEventHandler<MarketTOSQueryToUIEvent>
    {
        private readonly IMarketService _marketService;
        private readonly IEventBus _eventBus;
        private readonly ITOSMapper _mapper;

        public TOSService(IMarketService marketSever, IEventBus eventBus, ITOSMapper mapper)
        {
            _marketService = marketSever;
            _eventBus = eventBus;
            _mapper = mapper;
            Subscribe(_eventBus);
        }

        private void Subscribe(IEventBus eventBus)
        {
            eventBus.Subscribe<MarketL2DataIncomingToUIEvent>(this);
            eventBus.Subscribe<MarketTOSDataIncomingToUIEvent>(this);
            eventBus.Subscribe<MarketL2QueryToUIEvent>(this);
            eventBus.Subscribe<MarketTOSQueryToUIEvent>(this);
        }

        public async Task<Service.Market.IMarketObsever> SubscribeTosData(string symbol, IEventHandler<TOSDataIncomingEvent> eventHandler)
        {
           var obsever = await _marketService.SubscribeTosData(symbol);
            return obsever;
        }

        public async Task<Service.Market.IMarketObsever> SubscribeL2Data(string symbol, IEventHandler<L2DataIncomingEvent> eventHandler, int precision)
        {
            var obsever = await _marketService.SubscribeL2Data(symbol, precision);
            return obsever;
        }

        public async Task GetL2Data(string symbol)
        {
            await _marketService.GetL2Data(symbol);
        }

        public async Task GetTOSData(string symbol)
        {
            await _marketService.GetTOSData(symbol);
        }

        public async Task Handle(MarketTOSDataIncomingToUIEvent @event, CancellationToken token)
        {
            var tosDataIncomingEvent = _mapper.MapToTosIncomgingEvent(@event);
            await _eventBus.Publish(tosDataIncomingEvent, token);
        }

        public async Task Handle(MarketL2DataIncomingToUIEvent @event, CancellationToken token)
        {
            var l2DataIncomingEvent = _mapper.MapToL2IncomingEvent(@event);
            await _eventBus.Publish(l2DataIncomingEvent, token);
        }

        public async Task Handle(MarketL2QueryToUIEvent @event, CancellationToken token)
        {
            var l2QueryEvent = _mapper.MapToL2QueryEvent(@event);
            await _eventBus.Publish(l2QueryEvent, token);
        }

        public async Task Handle(MarketTOSQueryToUIEvent @event, CancellationToken token)
        {
            var tosQueryEvent = _mapper.MapToTOSQueryEvent(@event);
            await _eventBus.Publish(tosQueryEvent, token);
        }
    }
}
