using Liquidity2.Data.Client.Abstractions.Market.Events;
using Liquidity2.Service.DTO;
using Liquidity2.Service.Market.Events;
using System.Linq;

namespace Liquidity2.Service.Market
{
    public class MarketMapper : IMarketMapper
    {
        public MarketL2DataIncomingToUIEvent MapToL2IncomingEvent(MarketL2DataIncomingEvent l2DataIncomingEvent)
        {
            return new MarketL2DataIncomingToUIEvent(l2DataIncomingEvent.Symbol, l2DataIncomingEvent.T2Items.Select(data => MapToL2Item(data)), (TradeDirection)l2DataIncomingEvent.Side, l2DataIncomingEvent.Precision);
        }

        private DTO.L2Item MapToL2Item(Data.Client.Abstractions.DTO.L2Item data)
        {
            return new DTO.L2Item(data.Amount, data.Price, data.Count, (ExchangeType)data.Exchange);
        }

        public MarketL2QueryToUIEvent MapToL2QueryEvent(MarketL2QueryEvent l2QueryEvent)
        {
            return new MarketL2QueryToUIEvent(l2QueryEvent.Symbol, l2QueryEvent.BuyTradeItems.Select(data => MapToL2Item(data)), l2QueryEvent.SellTradeItems.Select(data => MapToL2Item(data)),l2QueryEvent.Precision);
        }

        public MarketTOSDataIncomingToUIEvent MapToTosIncomgingEvent(MarketTosDataIncomingEvent tosDataIncomingEvent)
        {
            return new MarketTOSDataIncomingToUIEvent(tosDataIncomingEvent.Symbol, tosDataIncomingEvent.TOSItems.Select(data => MapToTOSItem(data)));
        }

        private DTO.TOSItem MapToTOSItem(Data.Client.Abstractions.DTO.TOSItem data)
        {
            return new DTO.TOSItem(data.Amount, data.Price, (TradeDirection)data.Side, data.Time, (ExchangeType)data.ExchangeType);
        }

        public MarketTOSQueryToUIEvent MapToTOSQueryEvent(MarketTOSQueryEvent tOSQueryEvent)
        {
            return new MarketTOSQueryToUIEvent(tOSQueryEvent.Symbol, tOSQueryEvent.TOSItems.Select(data => MapToTOSItem(data)));
        }
    }
}
