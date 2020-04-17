using Liquidity2.Data.Client.Abstractions.Market.Events;
using Liquidity2.Service.Market.Events;

namespace Liquidity2.Service.Market
{
    public interface IMarketMapper
    {
        MarketTOSDataIncomingToUIEvent MapToTosIncomgingEvent(MarketTosDataIncomingEvent tosDataIncomingEvent);

        MarketL2DataIncomingToUIEvent MapToL2IncomingEvent(MarketL2DataIncomingEvent l2DataIncomingEvent);

        MarketL2QueryToUIEvent MapToL2QueryEvent(MarketL2QueryEvent l2QueryEvent);

        MarketTOSQueryToUIEvent MapToTOSQueryEvent(MarketTOSQueryEvent tOSQueryEvent);

    }
}
