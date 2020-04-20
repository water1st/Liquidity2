using Liquidity2.Data.Client.Abstractions.DTO;
using Liquidity2.Data.Client.Abstractions.Market.Events;
using Markets.Rpc.Protobuf;
using Markets.Rpc.Protobuf.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Liquidity2.Data.Client.Api.Queries.Market
{
    class MarketQueryMapper: IMarketQueryMapper
    {
        private static readonly IDictionary<Markets.Rpc.Protobuf.TradeDirection, Abstractions.DTO.TradeDirection> sideTypies;
        private static readonly IDictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType> exchangeTypies;

        static MarketQueryMapper()
        {
            sideTypies = new Dictionary<Markets.Rpc.Protobuf.TradeDirection, Abstractions.DTO.TradeDirection>();
            exchangeTypies = new Dictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType>();

            SideTypiesMappingInit(sideTypies);
            ExchangeTypiesMappingInit(exchangeTypies);
        }

        private static void ExchangeTypiesMappingInit(IDictionary<Markets.Rpc.Protobuf.ExchangeType, Abstractions.DTO.ExchangeType> exchangeTypies)
        {
            exchangeTypies.Add(Markets.Rpc.Protobuf.ExchangeType.Okex, Abstractions.DTO.ExchangeType.Okex);
            exchangeTypies.Add(Markets.Rpc.Protobuf.ExchangeType.Huobi, Abstractions.DTO.ExchangeType.Huobi);
            exchangeTypies.Add(Markets.Rpc.Protobuf.ExchangeType.Bitfinex, Abstractions.DTO.ExchangeType.Bitfinex);
        }

        private static void SideTypiesMappingInit(IDictionary<Markets.Rpc.Protobuf.TradeDirection, Abstractions.DTO.TradeDirection> sideTypies)
        {
            sideTypies.Add(Markets.Rpc.Protobuf.TradeDirection.Bid, Abstractions.DTO.TradeDirection.Buy);
            sideTypies.Add(Markets.Rpc.Protobuf.TradeDirection.Ask, Abstractions.DTO.TradeDirection.Sell);
            sideTypies.Add(Markets.Rpc.Protobuf.TradeDirection.Unkown, Abstractions.DTO.TradeDirection.Unkown);
        }

        public MarketL2QueryEvent MapToL2QueryEvent(string symbol, ResponseOrderBook orderBook, int precision)
        {
            return new MarketL2QueryEvent(symbol, orderBook.Bids.Select(data => MapToBookItem(data)), orderBook.Asks.Select(data => MapToBookItem(data)), precision);
        }

        public L2Item MapToBookItem(BookItem bookItem)
        {
            var item = new L2Item(Convert.ToDecimal(bookItem.Amount), Convert.ToDecimal(bookItem.Price), bookItem.Count, exchangeTypies[bookItem.Exchange]);
            return item;
        }

        public MarketTOSQueryEvent MapToTOSQueryEvent(string symbol, ResponseTrades trades)
        {
            return new MarketTOSQueryEvent(symbol, trades.Items.Select(data => MapToTradeItem(data)));
        }

        public TOSItem MapToTradeItem(TradeItem tradeItem)
        {
            var item = new TOSItem(Convert.ToDecimal(tradeItem.Amount), Convert.ToDecimal(tradeItem.Price), sideTypies[tradeItem.Side], DateTimeOffset.FromUnixTimeMilliseconds(tradeItem.UnixTimeStamp).ToLocalTime(), exchangeTypies[tradeItem.Exchange]);
            return item;
        }

        public MarketTickerQueryEvent MapToTicker(ResponseAllTickers tickers)
        {
            return new MarketTickerQueryEvent(tickers.Items.Select(data => MapToTicker(data)));
        }

        private Abstractions.DTO.Ticker MapToTicker(Markets.Rpc.Protobuf.Ticker data)
        {
            return new Abstractions.DTO.Ticker(data.Amount, data.Count, data.Open, data.Close, data.Low, data.High, data.Vol, data.Pair);
        }

        public MarketKLineMinQueryEvent MapToKLineQueryEvent(string symbol, ResponseCandles candles)
        {
            return new MarketKLineMinQueryEvent(symbol, candles.Items.Select(data => MapToKLineItem(data)));
        }

        private KLineItem MapToKLineItem(CandleItem data)
        {
            return new KLineItem(
                Convert.ToDecimal(data.Close),
                Convert.ToDecimal(data.High),
                Convert.ToDecimal(data.Low),
                Convert.ToDecimal(data.Open),
                Convert.ToDecimal(data.Turnover),
                Convert.ToDecimal(data.Volume),
                data.UnixTimeStamp);
        }

        public MarketKLineDayQueryEvent MapToKLineDayQueryEvent(string symbol, ResponseCandles candles)
        {
            return new MarketKLineDayQueryEvent(symbol, candles.Items.Select(data => MapToKLineItem(data)));
        }
    }
}
