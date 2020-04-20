using Liquidity2.UI.Components.Chart.Render.TimeLabel;
using Liquidity2.UI.Components.KLine.Model;
using System;
using System.Collections.Generic;

namespace Liquidity2.UI.Components.Chart.Render
{
    public class TimeLabelMapper : ITimeLabelMapper
    {

        private readonly IDictionary<KLineTimeSpan, Func<double, TimeLabelFormat>> TimeLabelFormatDictionary;

        private IList<DateTime> dateTimes;

        public TimeLabelMapper()
        {
            TimeLabelFormatDictionary = new Dictionary<KLineTimeSpan, Func<double, TimeLabelFormat>>() {
                {KLineTimeSpan.Time,ChooseMinFormat },
                {KLineTimeSpan.OneMin,ChooseMinFormat },
                {KLineTimeSpan.TenMin, ChooseHourFormat},
                {KLineTimeSpan.ThirtyMin,ChooseSixHourFormat},
                {KLineTimeSpan.OneDay,ChooseOneDayFormat }
            };
        }

        private TimeLabelFormat ChooseOneDayFormat(double stickWidth)
        {
            var indexs = new List<int>();
            if (stickWidth <= 10)
            {
                indexs = ChooseDayIndexs(1);
                return new TimeLabelFormat("M月", indexs);
            }
            else
            {
                indexs = ChooseDayIndexs(15);
                return new TimeLabelFormat("M月d日", indexs);
            }
        }

        private TimeLabelFormat ChooseSixHourFormat(double stickWidth)
        {
            var indexs = new List<int>();
            if (stickWidth <= 10)
            {
                indexs = ChooseHourIndexs(12);
            }
            else
            {
                indexs = ChooseHourIndexs(6);
            }
            return new TimeLabelFormat("HH:mm", indexs);
        }

        private TimeLabelFormat ChooseHourFormat(double stickWidth)
        {
            var indexs = new List<int>();
            if (stickWidth <= 3)
            {
                indexs = ChooseHourIndexs(12);
            }
            else if (stickWidth <= 10)
            {
                indexs = ChooseHourIndexs(6);
            }
            else
            {
                for (int i = 0; i < dateTimes.Count; i++)
                {
                    if (dateTimes[i].Minute == 0)
                    {
                        indexs.Add(i);
                    }
                }
            }
            return new TimeLabelFormat("HH:mm", indexs);
        }

        //根据分钟的区间选择时间的格式
        private TimeLabelFormat ChooseMinFormat(double stickWidth)
        {
            var indexs = new List<int>();
            if (stickWidth <= 3)
            {
                for (int i = 0; i < dateTimes.Count; i++)
                {
                    if (dateTimes[i].Minute == 0)
                    {
                        indexs.Add(i);
                    }
                }
            }
            else if (stickWidth <= 10)
            {
                indexs = ChooseMinIndexs(30);
            }
            else
            {
                indexs = ChooseMinIndexs(10);
            }
            return new TimeLabelFormat("HH:mm", indexs);
        }

        private List<int> ChooseHourIndexs(int hour)
        {
            var indexs = new List<int>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                if (dateTimes[i].Hour % hour == 0 && dateTimes[i].Minute == 0)
                {
                    indexs.Add(i);
                }
            }
            return indexs;
        }

        private List<int> ChooseDayIndexs(int day)
        {
            var indexs = new List<int>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                if (dateTimes[i].Day == 1 || dateTimes[i].Day == day)
                {
                    indexs.Add(i);
                }
            }
            return indexs;
        }

        private List<int> ChooseMinIndexs(int min)
        {
            var indexs = new List<int>();
            for (int i = 0; i < dateTimes.Count; i++)
            {
                if (dateTimes[i].Minute % min == 0 || dateTimes[i].Minute == 0)
                {
                    indexs.Add(i);
                }
            }
            return indexs;
        }

        public TimeLabelFormat MapToFormat(KLineTimeSpan timeSpan, double stickWidth, IList<DateTime> dateTimes)
        {
            this.dateTimes = dateTimes;
            var func = TimeLabelFormatDictionary[timeSpan];
            var format = func?.Invoke(stickWidth);
            return format;
        }
    }
}
