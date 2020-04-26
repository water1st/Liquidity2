using Liquidity2.UI.Components.Chart.Render.TimeLabel;
using Liquidity2.UI.Components.KLine.Model;
using System;
using System.Collections.Generic;

namespace Liquidity2.UI.Components.Chart.Render
{
    public interface ITimeLabelMapper
    {
        TimeLabelFormat MapToFormat(KLineTimeSpan timeSpan, double stickWidth, IList<DateTime> dateTimes);
    }
}
