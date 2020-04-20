using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.Data.Client.Abstractions.Market.SubscribeModel
{
    public class UnSubscribeModel : SubscribeModel
    {
        public UnSubscribeModel(string symbol, MarketSubscribeDataType type, int precision = 0) : base(symbol, type, precision)
        {
        }
    }
}
