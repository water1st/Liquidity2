using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.TOS.Events;
using System;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public class TOSService : ITOSService
    {
        public Task GetAllTickers()
        {
            throw new NotImplementedException();
        }

        public Task GetL2Data(string getL2DataRequest)
        {
            throw new NotImplementedException();
        }

        public Task GetTOSData(string getTOSDataRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IL2MarketObsever> SubscribeL2Data(string subscribeL2Request, IEventHandler<L2DataIncomingEvent> eventHandler, int precision = 0)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeTickerData()
        {
            throw new NotImplementedException();
        }

        public Task<ITOSMarketObsever> SubscribeTosData(string subscribeTosRequest, IEventHandler<TOSDataIncomingEvent> eventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
