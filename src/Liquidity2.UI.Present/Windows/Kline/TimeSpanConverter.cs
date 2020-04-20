using Liquidity2.UI.Components.KLine.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Liquidity2.UI.Present.Windows.Kline
{
    public class TimeSpanConverter : IValueConverter
    {

        private readonly IDictionary<KLineTimeSpan, string> TimeStringPairs = new Dictionary<KLineTimeSpan, string>
        {
            { KLineTimeSpan.Time,"Time" },
            { KLineTimeSpan.OneMin,"1m" },
            { KLineTimeSpan.TenMin,"10m" },
            { KLineTimeSpan.ThirtyMin,"30m" },
            { KLineTimeSpan.OneHour, "1H" },
            { KLineTimeSpan.SixHour,"6H" },
            { KLineTimeSpan.TwelveHour,"12H" },
            { KLineTimeSpan.OneDay,"1D" },
            { KLineTimeSpan.TwoDay,"2D" },
            { KLineTimeSpan.FourDay ,"4D" },
            { KLineTimeSpan.OneWeek,"1W" },
            { KLineTimeSpan.TwoWeek,"2W" },
            { KLineTimeSpan.FourWeek,"4W" },
            { KLineTimeSpan.OneMonth,"1M" },
            { KLineTimeSpan.ThreeMonth,"3M" },
            { KLineTimeSpan.SixMonth,"6M" }
        };


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IReadOnlyCollection<KLineTimeSpan> time)
            {
                var result = new List<string>();
                foreach (var item in time)
                {
                    result.Add(TimeStringPairs[item]);
                }
                return result;
            }

            if (value is KLineTimeSpan span)
            {
                var result = TimeStringPairs[span];
                return result;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
