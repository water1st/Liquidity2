using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.TOS.Events;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface ITOSService
    {
        Task<Service.Market.IMarketObsever> SubscribeTosData(string subscribeTosRequest, IEventHandler<TOSDataIncomingEvent> eventHandler);

        Task<Service.Market.IMarketObsever> SubscribeL2Data(string subscribeL2Request, IEventHandler<L2DataIncomingEvent> eventHandler, int precision = 0);

        Task GetL2Data(string getL2DataRequest);

        Task GetTOSData(string getTOSDataRequest);
    }
}
