using Liquidity2.UI.Services.DTO;
using Liquidity2.UI.Services.TOS.DTO;
using Liquidity2.UI.Windows.TOS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liquidity2.UI.Windows.TOS
{
    public interface ITOSWindowDataMapper
    {
        PlaceOrderEvent MapToPlaceOrderEvent(string fromCurrency, string toCurrency, L2Data data, TradeDirection tradeDirection);

        string MapToPrecisionString(int precision);

        int MapToPrecisionInt(string precisionString);

        L2Data MapToL2(L2Item bookItem, int precision);

        TOSData MapToTOS(TOSItem tradeItem);
    }
}
