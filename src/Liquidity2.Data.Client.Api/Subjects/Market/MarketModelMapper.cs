using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Data.Client.Abstractions.Market.Events;
using Markets.Rpc.Protobuf;
using Markets.Rpc.Protobuf.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExchangeType = Markets.Rpc.Protobuf.ExchangeType;
using Ticker = Markets.Rpc.Protobuf.Ticker;
using TradeDirection = Markets.Rpc.Protobuf.TradeDirection;

namespace Liquidity2.Data.Client.Api.Subjects.Market
{
    public class MarketModelMapper : IMarketModelMapper
    {

        private static readonly IDictionary<Markets.Rpc.Protobuf.TradeDirection, Abstractions.DTO.TradeDirection> sideTypies;
        private static readonly IDictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType> exchangeTypies;


        static MarketModelMapper()
        {
            sideTypies = new Dictionary<Markets.Rpc.Protobuf.TradeDirection, Abstractions.DTO.TradeDirection>();
            exchangeTypies = new Dictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType>();

            SideTypiesMappingInit(sideTypies);
            ExchangeTypiesMappingInit(exchangeTypies);
        }

        private static void ExchangeTypiesMappingInit(IDictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType> exchangeTypies)
        {
            exchangeTypies.Add(ExchangeType.Okex, Abstractions.DTO.ExchangeType.Okex);
            exchangeTypies.Add(ExchangeType.Huobi, Abstractions.DTO.ExchangeType.Huobi);
            exchangeTypies.Add(ExchangeType.Bitfinex, Abstractions.DTO.ExchangeType.Bitfinex);
        }

        private static void SideTypiesMappingInit(IDictionary<TradeDirection, Abstractions.DTO.TradeDirection> sideTypies)
        {
            sideTypies.Add(TradeDirection.Bid, Abstractions.DTO.TradeDirection.Buy);
            sideTypies.Add(TradeDirection.Ask, Abstractions.DTO.TradeDirection.Sell);
            sideTypies.Add(TradeDirection.Unkown, Abstractions.DTO.TradeDirection.Unkown);
        }

        public MarketTosDataIncomingEvent MapToTos(RawTrade rawTrade, string symbol)
        {
            return new MarketTosDataIncomingEvent(symbol: symbol, tosItems: rawTrade.Items.Select(data => MapToTradeItem(data)));
        }

        public TOSItem MapToTradeItem(TradeItem tradeItem)
        {
            var item = new TOSItem(Convert.ToDecimal(tradeItem.Amount), Convert.ToDecimal(tradeItem.Price), sideTypies[tradeItem.Side], DateTimeOffset.FromUnixTimeMilliseconds(tradeItem.UnixTimeStamp).ToLocalTime(), exchangeTypies[tradeItem.Exchange]);
            return item;
        }

        public MarketL2DataIncomingEvent MapToL2(BookSanpshot bookSanpshot, string symbol, int precision)
        {
            return new MarketL2DataIncomingEvent(symbol, bookSanpshot.Items.Select(data => MapToBookItem(data)), sideTypies[bookSanpshot.Direction], precision);
        }

        public L2Item MapToBookItem(BookItem bookItem)
        {
            var item = new L2Item(Convert.ToDecimal(bookItem.Amount), Convert.ToDecimal(bookItem.Price), bookItem.Count, exchangeTypies[bookItem.Exchange]);
            return item;
        }

        public MarketTickerDataIncomingEvent MapToTickers(TickerList tickers)
        {
            return new MarketTickerDataIncomingEvent(tickers.Items.Select(data => MapToTicker(data)));
        }

        private Abstractions.DTO.Ticker MapToTicker(Ticker data)
        {
            return new Abstractions.DTO.Ticker(data.Amount, data.Count, data.Open, data.Close, data.Low, data.High, data.Vol, data.Pair);
        }

        public MarketCandleDataIncomingEvent MapToCandle(RawCandle rawCandle, string symbol)
        {
            return new MarketCandleDataIncomingEvent(new KLineItem(Convert.ToDecimal(rawCandle.Item.Close), Convert.ToDecimal(rawCandle.Item.High), Convert.ToDecimal(rawCandle.Item.Low), Convert.ToDecimal(rawCandle.Item.Open), Convert.ToDecimal(rawCandle.Item.Turnover), Convert.ToDecimal(rawCandle.Item.Volume), rawCandle.Item.UnixTimeStamp), symbol);
        }
    }
}
