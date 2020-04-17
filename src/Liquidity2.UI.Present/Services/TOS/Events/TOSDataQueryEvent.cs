using Liquidity2.Extensions.EventBus;
using Liquidity2.UI.Services.TOS.DTO;
using System.Collections.Generic;

namespace Liquidity2.UI.Services.TOS.Events
{
    public class TOSDataQueryEvent:Event
    {
        public TOSDataQueryEvent(string symbol, IEnumerable<TOSItem> tosItems)
        {
            Symbol = symbol;
            TOSItems = tosItems;
        }

        /// <summary>
        /// 代码
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// TOS集合
        /// </summary>
        public IEnumerable<TOSItem> TOSItems { get; }

    }
}
