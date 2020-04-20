using Liquidity2.UI.Components.KLine.Model;
using System.Collections.Generic;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public class KLineDataMapper : IKLineDataMapper
    {
        private readonly IDictionary<string, KLineTimeSpan> TimeStringPairs = new Dictionary<string, KLineTimeSpan>
        {
            {"Time",KLineTimeSpan.Time},
            {"1m",KLineTimeSpan.OneMin},
            {"5m" ,KLineTimeSpan.FiveMin},
            {"10m",KLineTimeSpan.TenMin},
            {"30m" ,KLineTimeSpan.ThirtyMin},
            {"1H",KLineTimeSpan.OneHour},
            {"6H",KLineTimeSpan.SixHour},
            {"12H",KLineTimeSpan.TwelveHour},
            {"1D",KLineTimeSpan.OneDay },
            {"2D",KLineTimeSpan.TwoDay},
            {"4D",KLineTimeSpan.FourDay },
            {"1W",KLineTimeSpan.OneWeek },
            {"2W" ,KLineTimeSpan.TwoWeek},
            {"4W",KLineTimeSpan.FourWeek },
            {"1M" ,KLineTimeSpan.OneMonth},
            {"3M" ,KLineTimeSpan.ThreeMonth},
            {"6M",KLineTimeSpan.SixMonth }
        };

        public KLineTimeSpan MapToTimeSpan(string timeSpan)
        {
            return TimeStringPairs[timeSpan];
        }
    }
}
