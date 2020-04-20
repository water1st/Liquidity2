using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine.Model;
using Liquidity2.UI.Components.Volume.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Volume.Interface
{
    public interface IVolumeChart : IChart
    {
        Thickness Margin { get; set; }

        Typeface Typeface { get; set; }

        double FontSize { get; set; }

        double VolumeStickWidth { get; set; }

        double VolumeStickMargin { get; set; }

        double YAxisCoordinateCount { get; set; }

        double YAxisCoordinateMaxValue { get; }

        double YAxisCoordinateMinValue { get; }

        double YAxisValueRangeAndDataPeakScale { get; set; }

        Brush AxisBrush { get; set; }

        Brush AxisLabelBrush { get; set; }

        Brush ReferenceLineBrush { get; set; }

        Brush RiseBrush { get; set; }

        Brush FallBrush { get; set; }

        Brush VolumeTextBrush { get; set; }

        int VolumeViewAreaStartIndex { get; }

        int VolumeViewAreaEndIndex { get; }

        IList<VolumeItem> VolumeItems { get; set; }

        IList<decimal> Lines { get; set; }

        IList<Type> SelectRenders { get; set; }

        /// <summary>
        /// Y轴极差
        /// </summary>
        double YAxisValueRange { get; }

        /// <summary>
        /// X轴真实宽度
        /// </summary>
        double XAxisActualWidth { get; }

        /// <summary>
        /// Y轴真实高度
        /// </summary>
        double YAxisActualHeight { get; }

        /// <summary>
        /// 差值与像素比例
        /// 每点像素所表达的差值
        /// </summary>
        double YAxisCoordinateAndPixelProportion { get; }
        /// <summary>
        /// 可视数据宽
        /// </summary>
        double VisualRrangeDataWidth { get; }
        /// <summary>
        /// 可视数据在X轴控件占比
        /// </summary>
        double VisualRrangeDataWidthInXAxisActualWidthProportion { get; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        KLineTimeSpan TimeSpan { get; set; }

        void DrawNewest();

        void Clear();

        void LinkageMoveLeft();

        void LinkageMoveRight();

        void LinkageZoomIn();

        void LinkageZoomOut();

        void LinkageMove();
    }
}
