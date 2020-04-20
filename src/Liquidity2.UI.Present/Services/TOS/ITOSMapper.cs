using Liquidity2.Service.Market.Events;
using Liquidity2.UI.Services.TOS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI.Services.TOS
{
    public interface ITOSMapper
    {

        TOSDataIncomingEvent MapToTosIncomgingEvent(MarketTOSDataIncomingToUIEvent tosDataIncomingEvent);

        L2DataIncomingEvent MapToL2IncomingEvent(MarketL2DataIncomingToUIEvent l2DataIncomingEvent);

        L2DataQueryEvent MapToL2QueryEvent(MarketL2QueryToUIEvent l2QueryEvent);

        TOSDataQueryEvent MapToTOSQueryEvent(MarketTOSQueryToUIEvent tOSQueryEvent);

    }
}
