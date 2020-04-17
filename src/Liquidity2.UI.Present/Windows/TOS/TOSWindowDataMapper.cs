using Liquidity2.UI.Services.DTO;
using Liquidity2.UI.Services.TOS.DTO;
using Liquidity2.UI.Windows.TOS.Events;
using System.Collections.Generic;

namespace Liquidity2.UI.Windows.TOS
{
    public class TOSWindowDataMapper : ITOSWindowDataMapper
    {

        private static readonly IDictionary<int, string> stringChange;

        static TOSWindowDataMapper()
        {
            stringChange = new Dictionary<int, string>();
            StringMappingInit(stringChange);
        }

        private static void StringMappingInit(IDictionary<int, string> stringChange)
        {
            stringChange.Add(8, "8位小数");
            stringChange.Add(7, "7位小数");
            stringChange.Add(6, "6位小数");
            stringChange.Add(5, "5位小数");
            stringChange.Add(4, "4位小数");
            stringChange.Add(3, "3位小数");
            stringChange.Add(2, "2位小数");
            stringChange.Add(1, "1位小数");
            stringChange.Add(0, "1位整数");
            stringChange.Add(-1, "2位整数");
            stringChange.Add(-2, "3位整数");
            stringChange.Add(-3, "4位整数");
            stringChange.Add(-4, "5位整数");
        }

        public PlaceOrderEvent MapToPlaceOrderEvent(string fromCurrency, string toCurrency, L2Data data, TradeDirection tradeDirection)
        {
            return new PlaceOrderEvent(fromCurrency, toCurrency, data.Price, data.Exchange, tradeDirection);
        }

        public L2Data MapToL2(L2Item bookItem, int precision)
        {
            string priceDecimalStr = bookItem.Price.ToString($"F{precision}");
            string amountDecimalStr = bookItem.Amount.ToString();
            L2Data l2Data = new L2Data(decimal.Parse(priceDecimalStr), decimal.Parse(amountDecimalStr), bookItem.Count, bookItem.Exchange);
            return l2Data;
        }

        public TOSData MapToTOS(TOSItem tradeItem)
        {
            string priceDecimalStr = tradeItem.Price.ToString();
            string amountDecimalStr = tradeItem.Amount.ToString("0.######");
            TOSData tOSData = new TOSData(decimal.Parse(priceDecimalStr), decimal.Parse(amountDecimalStr), tradeItem.Time.ToString("HH:mm:ss"), tradeItem.Side, tradeItem.ExchangeType);
            return tOSData;
        }

        public int MapToPrecisionInt(string precisionString)
        {
            var precision = 0;
            foreach (KeyValuePair<int, string> item in stringChange)
            {
                if (item.Value.Equals(precisionString))
                {
                    precision = item.Key;
                }
            }
            return precision;
        }

        public string MapToPrecisionString(int precision)
        {
            return stringChange[precision];
        }
    }
}
