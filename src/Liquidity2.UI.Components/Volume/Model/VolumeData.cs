using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Volume.Model
{
    public class VolumeData : DataBase, I2DChartData<VolumeItem>
    {
        public IList<VolumeItem> DataSources { get; set; }
        public double ActualHeight { get; set; }
        public double YAxisCoordinateMinValue { get; set; }
        public double DataWidth { get; set; }
        public double DataMargin { get; set; }
        public int VisualRangeEndIndex { get; set; }
        public int VisualRangeStartIndex { get; set; }
        public double YAxisCoordinateAndPixelProportion { get; set; }
        public Thickness Margin { get; set; }
        public Brush RiseBrush { get; set; }
        public Brush FallBrush { get; set; }
    }
}
