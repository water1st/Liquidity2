using Liquidity2.UI.Components.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Data
{
    public class LineData : DataBase, I2DChartData<LineDataItem>
    {
        public IList<LineDataItem> DataSources { get; set; }
        public double ActualHeight { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public int VisualRangeEndIndex { get; set; }
        public int VisualRangeStartIndex { get; set; }
        public double YAxisCoordinateAndPixelProportion { get; set; }
        public Thickness Margin { get; set; }
    }

    public class LineDataItem
    {
        public IList<decimal> Datas { get; set; }

        public Brush Brush { get; set; }
    }
}
