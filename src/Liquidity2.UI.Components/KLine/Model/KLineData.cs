using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.KLine.Model
{
    public class KLineData : DataBase, I2DChartData<OHLC>
    {
        public double YAxisCoordinateAndPixelProportion { get; set; }
        public double ActualHeight { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public Thickness Margin { get; set; }
        public Brush RiseBrush { get; set; }
        public Brush FallBrush { get; set; }
        public IDictionary<Visual, KlineMappingData> Mapping { get; set; }
        public IList<OHLC> DataSources { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public int VisualRangeEndIndex { get; set; }
        public int VisualRangeStartIndex { get; set; }
    }
}
