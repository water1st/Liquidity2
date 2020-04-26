using Liquidity2.UI.Components.Model;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.KLine.Model
{
    public class KlineInfoData : DataBase
    {
        public OHLC OHLC { get; set; }
        public Point MidLineLowPoint { get; set; }
        public Point MidLineHeightPoint { get; set; }
        public double XAxisActualWidth { get; set; }
        public double YAxisActualHeight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Brush Brush { get; set; }
        public Brush BorderBrush { get; set; }
        public Brush RiseBrush { get; set; }
        public Brush FallBrush { get; set; }
        public Typeface Typeface { get; set; }
        public double FontSize { get; set; }
        public bool Render { get; set; }
        public double XMargin { get; set; }
        public double YMargin { get; set; }
    }
}
