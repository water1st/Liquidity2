using System.Windows;

namespace Liquidity2.UI.Components.KLine.Model
{
    public class KlineMappingData
    {
        public OHLC OHLC { get; set; }
        public Point MidLineLowPoint { get; set; }
        public Point MidLineHeightPoint { get; set; }
    }
}
