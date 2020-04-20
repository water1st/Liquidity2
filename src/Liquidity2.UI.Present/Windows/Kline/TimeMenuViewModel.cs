using Liquidity2.UI.Components.KLine.Model;
using System.Collections.Generic;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public class TimeMenuViewModel
    {
        public KLineTimeSpan Time { get; set; } = KLineTimeSpan.Time;

        public KLineTimeSpan OneMin { get; set; } = KLineTimeSpan.OneMin;

        public KLineTimeSpan OneDay { get; set; } = KLineTimeSpan.OneDay;

        public IReadOnlyCollection<KLineTimeSpan> MinuteListData => minuteListData;

        private KLineTimeSpan[] minuteListData = { KLineTimeSpan.OneMin };

        public IReadOnlyCollection<KLineTimeSpan> HourListData => hourListData;

        private KLineTimeSpan[] hourListData = { KLineTimeSpan.OneHour, KLineTimeSpan.SixHour, KLineTimeSpan.TwelveHour };

        public IReadOnlyCollection<KLineTimeSpan> DayListData => dayListData;

        private KLineTimeSpan[] dayListData = { KLineTimeSpan.OneDay };

        public IReadOnlyCollection<KLineTimeSpan> WeekListData => weekListData;

        private KLineTimeSpan[] weekListData = { KLineTimeSpan.OneWeek, KLineTimeSpan.TwoWeek, KLineTimeSpan.FourWeek };

        public IReadOnlyCollection<KLineTimeSpan> MonthListData => monthListData;

        private KLineTimeSpan[] monthListData = { KLineTimeSpan.OneMonth, KLineTimeSpan.ThreeMonth, KLineTimeSpan.SixMonth };
    }
}
