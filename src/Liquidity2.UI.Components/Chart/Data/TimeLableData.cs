using Liquidity2.UI.Components.KLine.Model;
using Liquidity2.UI.Components.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Data
{
    public class TimeLableData : DataBase
    {
        public double XAxisCoordinateCount { get; set; }
        public double XAxisValueRange { get; set; }
        public double XAxisCoordinateMinValue { get; set; }
        public double XAxisActualWidth { get; set; }
        public double StickWidth { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public IList<DateTime> DateTimes { get; set; }
        public double FontSize { get; set; }
        public double ActualHeight { get; set; }
        public Typeface Typeface { get; set; }
        public Brush Brush { get; set; }
        public Thickness Margin { get; set; }
        public Brush DateBrush { get; set; }
        public KLineTimeSpan TimeSpan { get; set; }
    }
}
