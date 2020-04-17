using Liquidity2.Service.Market.Events;
using Liquidity2.UI.Services.DTO;
using Liquidity2.UI.Services.TOS.DTO;
using Liquidity2.UI.Services.TOS.Events;
using System.Linq;

namespace Liquidity2.UI.Services.TOS
{
    public class TOSMapper : ITOSMapper
    {
        public L2DataIncomingEvent MapToL2IncomingEvent(MarketL2DataIncomingToUIEvent l2DataIncomingEvent)
        {
            return new L2DataIncomingEvent(l2DataIncomingEvent.Symbol, l2DataIncomingEvent.T2Items.Select(data => MapToL2Item(data)), (TradeDirection)l2DataIncomingEvent.Side, l2DataIncomingEvent.Precision);
        }

        private DTO.L2Item MapToL2Item(Service.Market.DTO.L2Item data)
        {
            return new DTO.L2Item(data.Amount, data.Price, data.Count, (ExchangeType)data.Exchange);
        }

        public L2DataQueryEvent MapToL2QueryEvent(MarketL2QueryToUIEvent l2QueryEvent)
        {
            return new L2DataQueryEvent(l2QueryEvent.Symbol, l2QueryEvent.BuyTradeItems.Select(data => MapToL2Item(data)), l2QueryEvent.SellTradeItems.Select(data => MapToL2Item(data)), l2QueryEvent.Precision);
        }

        public TOSDataIncomingEvent MapToTosIncomgingEvent(MarketTOSDataIncomingToUIEvent tosDataIncomingEvent)
        {
            return new TOSDataIncomingEvent(tosDataIncomingEvent.Symbol, tosDataIncomingEvent.TOSItems.Select(data => MapToTOSItem(data)));
        }

        private TOSItem MapToTOSItem(Service.Market.DTO.TOSItem data)
        {
            return new TOSItem(data.Amount, data.Price, (TradeDirection)data.Side, data.Time, (ExchangeType)data.ExchangeType);
        }

        public TOSDataQueryEvent MapToTOSQueryEvent(MarketTOSQueryToUIEvent tOSQueryEvent)
        {
            return new TOSDataQueryEvent(tOSQueryEvent.Symbol, tOSQueryEvent.TOSItems.Select(data => MapToTOSItem(data)));
        }
    }
}
