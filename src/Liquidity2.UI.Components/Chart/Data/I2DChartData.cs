using System.Collections.Generic;
using System.Windows;

namespace Liquidity2.UI.Components.Chart.Data
{
    public interface I2DChartData<TSourceItem>
    {
        public IList<TSourceItem> DataSources { get; set; }
        public double ActualHeight { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public int VisualRangeEndIndex { get; set; }
        public int VisualRangeStartIndex { get; set; }
        public double YAxisCoordinateAndPixelProportion { get; set; }
        public Thickness Margin { get; set; }
    }
}
