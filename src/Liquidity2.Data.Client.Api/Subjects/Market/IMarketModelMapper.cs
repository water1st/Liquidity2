using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Data.Client.Abstractions.Market.Events;
using Markets.Rpc.Protobuf;
using Markets.Rpc.Protobuf.Subscribe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.Data.Client.Api.Subjects.Market
{
    public interface IMarketModelMapper
    {
        MarketTosDataIncomingEvent MapToTos(RawTrade rawTrade, string symbol);
        MarketL2DataIncomingEvent MapToL2(BookSanpshot bookSanpshot, string symbol, int precision);
        MarketTickerDataIncomingEvent MapToTickers(TickerList tickers);

        MarketCandleDataIncomingEvent MapToCandle(RawCandle rawCandle, string symbol);

        L2Item MapToBookItem(BookItem bookItem);
        TOSItem MapToTradeItem(TradeItem tradeItem);
    }
}
