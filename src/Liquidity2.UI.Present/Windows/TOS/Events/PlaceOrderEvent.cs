using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.DTO;

namespace Liquidity2.UI.Windows.TOS.Events
{
    public class PlaceOrderEvent:Event
    {
        public PlaceOrderEvent(string toCurrency, string fromCurrency, decimal price, ExchangeType exchange, TradeDirection tradeDirection)
        {
            ToCurrency = toCurrency;
            FromCurrency = fromCurrency;
            Exchange = exchange;
            Price = price;
            TradeDirection = tradeDirection;
        }

        /// <summary>
        /// 买入币种
        /// </summary>
        public string ToCurrency { get; }

        /// <summary>
        /// 卖出币种
        /// </summary>
        public string FromCurrency { get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// 交易所
        /// </summary>
        public ExchangeType Exchange { get; }

        public TradeDirection TradeDirection { get; set; }

    }
}
