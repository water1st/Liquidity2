using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Model
{
    public class AxisLineData : DataBase
    {
        public double ActualWidth { get; set; }
        public double ActualHeight { get; set; }
        public double YAxisCoordinateCount { get; set; }
        public double YAxisActualHeight => ActualHeight - Margin.Top - Margin.Bottom;
        public Thickness Margin { get; set; }
        public Brush AxisBrush { get; set; }
        public Brush ReferenceLineBrush { get; set; }
    }
}
