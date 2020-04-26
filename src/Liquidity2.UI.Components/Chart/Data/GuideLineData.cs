using Liquidity2.UI.Components.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Data
{
    public class GuideLineData : DataBase
    {
        public double XAxisCoordinateCount { get; set; }
        public double YAxisValueRange { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public double ActualWidth { get; set; }
        public double StickWidth { get; set; }
        public string TextFormat { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public Point MousePoint { get; set; }
        public int ViewAreaStartIndex { get; set; }
        public IList<DateTime> Times { get; set; }
        public int ViewAreaEndIndex { get; set; }
        public Cursor MouseCursor { get; set; }
        public double FontSize { get; set; }
        public double ActualHeight { get; set; }
        public Typeface Typeface { get; set; }
        public Brush Brush { get; set; }
        public Brush TextBrush { get; set; }
        public Brush RectengelBrush { get; set; }
        public Thickness Margin { get; set; }
    }
}
