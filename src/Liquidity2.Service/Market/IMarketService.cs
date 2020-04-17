using Liquidity2.Service.Market.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liquidity2.Service.Market
{
    public interface IMarketService
    {
        Task SubscribeTosData(string symbol);

        Task SubscribeL2Data(string symbol, int precision = 0);

        Task SubscribeTickerData();

        Task GetL2Data(string symbol);

        Task GetTOSData(string symbol);

        Task GetAllTickers();

        Task Unsubscribe(string symbol, MarketSubscribeDataType type, int precision = 0);
    }
}
