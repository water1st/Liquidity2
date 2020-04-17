using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.Service.Market.DTO
{
    public enum MarketSubscribeDataType
    {
        /// <summary>
        /// L2类型
        /// </summary>
        L2Item = 0,
        /// <summary>
        /// K线
        /// </summary>
        CandleItem = 1,
        /// <summary>
        /// TOS
        /// </summary>
        TOSItem = 2
    }
}
