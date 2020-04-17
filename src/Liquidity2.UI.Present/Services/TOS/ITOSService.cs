using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.TOS.Events;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services.TOS
{
    public interface ITOSService
    {

        Task<IMarketObsever> SubscribeTosData(string subscribeTosRequest, IEventHandler<TOSDataIncomingEvent> eventHandler);

        Task<IMarketObsever> SubscribeL2Data(string subscribeL2Request, IEventHandler<L2DataIncomingEvent> eventHandler, int precision = 0);

        Task SubscribeTickerData();

        Task GetL2Data(string getL2DataRequest);

        Task GetTOSData(string getTOSDataRequest);

        Task GetAllTickers();
    }
}
