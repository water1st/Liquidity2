using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Model
{
    public class AxisLableData : DataBase
    {
        public double YAxisCoordinateCount { get; set; }
        public double YAxisValueRange { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public string TextFormat { get; set; }

        public double YAxisActualHeight { get; set; }
        public double FontSize { get; set; }
        public double ActualWidth { get; set; }
        public Typeface Typeface { get; set; }
        public Brush Brush { get; set; }
        public Thickness Margin { get; set; }
    }
}
