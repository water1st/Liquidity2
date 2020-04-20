using Liquidity2.UI.Components.Chart;
using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine.Model;
using Liquidity2.UI.Components.Model;
using Liquidity2.UI.Components.Volume.Interface;
using Liquidity2.UI.Components.Volume.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Volume
{
    public class Volume : ChartBase<IVolumeChart>,
        IVolumeChart,
        IHasVolumeData,
        IHasAxisLineData,
        IHasAxisLableData,
        IHasTimeLableData,
        IHasGuideLineData,
        IHasLineData
    {
        public static readonly DependencyProperty AxisBrushProperty = DependencyProperty.Register(nameof(AxisBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty AxisLabelBrushProperty = DependencyProperty.Register(nameof(AxisLabelBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty ReferenceLineBrushProperty = DependencyProperty.Register(nameof(ReferenceLineBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty RiseBrushProperty = DependencyProperty.Register(nameof(RiseBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty FallBrushProperty = DependencyProperty.Register(nameof(FallBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty DateBrushProperty = DependencyProperty.Register(nameof(DateBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty TypefaceProperty = DependencyProperty.Register(nameof(Typeface), typeof(Typeface), typeof(Volume));

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(Volume));

        public static readonly DependencyProperty YAxisCoordinateCountProperty = DependencyProperty.Register(nameof(YAxisCoordinateCount), typeof(double), typeof(Volume));

        public static readonly DependencyProperty YAxisValueRangeAndDataPeakScaleProperty = DependencyProperty.Register(nameof(YAxisValueRangeAndDataPeakScale), typeof(double), typeof(Volume));

        public static readonly DependencyProperty VolumestickWidthProperty = DependencyProperty.Register(nameof(VolumeStickWidth), typeof(double), typeof(Volume));

        public static readonly DependencyProperty VolumestickMarginProperty = DependencyProperty.Register(nameof(VolumeStickMargin), typeof(double), typeof(Volume));

        public static readonly DependencyProperty SelectRenderProperty =
            DependencyProperty.Register(nameof(SelectRenders), typeof(IList<Type>), typeof(Volume));

        public static readonly DependencyProperty VolumeTextBrushProperty =
           DependencyProperty.Register(nameof(VolumeTextBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty LinkageMoveLeftProperty = DependencyProperty.Register(nameof(MoveLeftCmd), typeof(ICommand), typeof(Volume));

        public static readonly DependencyProperty LinkageMoveRightProperty = DependencyProperty.Register(nameof(MoveRightCmd), typeof(ICommand), typeof(Volume));

        public static readonly DependencyProperty LinkageZoomInProperty = DependencyProperty.Register(nameof(ZoomInCmd), typeof(ICommand), typeof(Volume));

        public static readonly DependencyProperty LinkageZoomOutProperty = DependencyProperty.Register(nameof(ZoomOutCmd), typeof(ICommand), typeof(Volume));

        public static readonly DependencyProperty MouseMoveProperty = DependencyProperty.Register(nameof(MouseMoveCmd), typeof(ICommand), typeof(Volume));

        public static readonly DependencyProperty TextBrushProperty =
            DependencyProperty.Register(nameof(TextBrush), typeof(Brush), typeof(Volume));

        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register(nameof(TextFormat), typeof(string), typeof(Volume));

        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        public Brush TextBrush
        {
            get { return (Brush)GetValue(TextBrushProperty); }
            set { SetValue(TextBrushProperty, value); }
        }

        public ICommand MoveLeftCmd
        {
            get { return (ICommand)GetValue(LinkageMoveLeftProperty); }
            set { SetValue(LinkageMoveLeftProperty, value); }
        }

        public ICommand MoveRightCmd
        {
            get { return (ICommand)GetValue(LinkageMoveRightProperty); }
            set { SetValue(LinkageMoveRightProperty, value); }
        }

        public ICommand ZoomInCmd
        {
            get { return (ICommand)GetValue(LinkageZoomInProperty); }
            set { SetValue(LinkageZoomInProperty, value); }
        }

        public ICommand ZoomOutCmd
        {
            get { return (ICommand)GetValue(LinkageZoomOutProperty); }
            set { SetValue(LinkageZoomOutProperty, value); }
        }

        public ICommand MouseMoveCmd
        {
            get { return (ICommand)GetValue(MouseMoveProperty); }
            set { SetValue(MouseMoveProperty, value); }
        }

        public Typeface Typeface
        {
            get => (Typeface)GetValue(TypefaceProperty);
            set => SetValue(TypefaceProperty, value);
        }
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public double VolumeStickWidth
        {
            get => (double)GetValue(VolumestickWidthProperty);
            set => SetValue(VolumestickWidthProperty, value);
        }

        public double VolumeStickMargin
        {
            get => (double)GetValue(VolumestickMarginProperty);
            set => SetValue(VolumestickMarginProperty, value);
        }

        public double YAxisCoordinateCount
        {
            get => (double)GetValue(YAxisCoordinateCountProperty);
            set => SetValue(YAxisCoordinateCountProperty, value);
        }

        public double YAxisCoordinateMaxValue { get; private set; }

        public double YAxisCoordinateMinValue { get; private set; }

        public double YAxisValueRangeAndDataPeakScale
        {
            get => (double)GetValue(YAxisValueRangeAndDataPeakScaleProperty);
            set => SetValue(YAxisValueRangeAndDataPeakScaleProperty, value);
        }

        public Brush AxisBrush
        {
            get => (Brush)GetValue(AxisBrushProperty);
            set => SetValue(AxisBrushProperty, value);
        }

        public Brush AxisLabelBrush
        {
            get => (Brush)GetValue(AxisLabelBrushProperty);
            set => SetValue(AxisLabelBrushProperty, value);
        }

        public Brush ReferenceLineBrush
        {
            get => (Brush)GetValue(ReferenceLineBrushProperty);
            set => SetValue(ReferenceLineBrushProperty, value);
        }

        public Brush RiseBrush
        {
            get => (Brush)GetValue(RiseBrushProperty);
            set => SetValue(RiseBrushProperty, value);
        }

        public Brush FallBrush
        {
            get => (Brush)GetValue(FallBrushProperty);
            set => SetValue(FallBrushProperty, value);
        }

        public Brush DateBrush
        {
            get => (Brush)GetValue(DateBrushProperty);
            set => SetValue(DateBrushProperty, value);
        }

        public IList<Type> SelectRenders
        {
            get => (IList<Type>)GetValue(SelectRenderProperty);
            set => SetValue(SelectRenderProperty, value);
        }

        public Brush VolumeTextBrush
        {
            get => (Brush)GetValue(VolumeTextBrushProperty);
            set => SetValue(VolumeTextBrushProperty, value);
        }


        public int VolumeViewAreaStartIndex
        {
            get => _volumeViewAreaStartIndex;
            private set
            {
                if (VolumeItems != null)
                {
                    if (value <= 0)
                    {
                        _volumeViewAreaStartIndex = 0;
                    }
                    else
                    {
                        _volumeViewAreaStartIndex = value;
                    }

                    if (value >= VolumeItems.Count - 10)
                    {
                        _volumeViewAreaStartIndex = VolumeItems.Count - 10;
                    }
                }
            }
        }

        public int VolumeViewAreaEndIndex
        {
            get
            {
                if (VolumeItems == null)
                {
                    return 0;
                }

                var pointWidth = VolumeStickWidth + VolumeStickMargin * 2;
                var pointCount = (int)(XAxisActualWidth / pointWidth) - 1;
                var result = VolumeViewAreaStartIndex + pointCount;
                result = result > VolumeItems.Count - 1 ? VolumeItems.Count - 1 : result;
                return result;
            }
        }

        public double VisualRrangeDataWidth
        {
            get
            {
                if (VolumeItems == null)
                    return 0;

                var pointCount = VolumeViewAreaEndIndex - VolumeViewAreaStartIndex;
                var width = pointCount * (VolumeStickWidth + VolumeStickMargin * 2);
                return width;
            }
        }

        private int _volumeViewAreaStartIndex;

        public IList<VolumeItem> VolumeItems { get; set; }
        public IList<decimal> Lines { get; set; }

        public KLineTimeSpan TimeSpan { get; set; }

        public double YAxisValueRange => YAxisCoordinateMaxValue - YAxisCoordinateMinValue;

        public double XAxisActualWidth => ActualWidth - Margin.Left - Margin.Right;

        public double YAxisActualHeight => ActualHeight - Margin.Top - Margin.Bottom;

        public double YAxisCoordinateAndPixelProportion => YAxisActualHeight / YAxisValueRange;

        public double VisualRrangeDataWidthInXAxisActualWidthProportion => VisualRrangeDataWidth / XAxisActualWidth;

        private Point MousePoint
        {
            get => Mouse.GetPosition(this);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            Proxy.SetProxy(this);
        }

        public override void Draw()
        {
            if (VolumeItems == null)
            {
                return;
            }

            ComputeVisualRrangeValues();
            foreach (IRender render in Renders)
            {
                if (SelectRenders.ToList().FindIndex(x => x == render.GetType()) != -1)
                {
                    render.Render(this);
                }
            }
        }

        private void ComputeVisualRrangeValues()
        {
            if (VolumeItems == null)
            {
                return;
            }

            var max = double.MinValue;
            var min = ActualHeight - Margin.Bottom;
            for (int i = VolumeViewAreaStartIndex; i <= VolumeViewAreaEndIndex; i++)
            {
                max = Math.Max(Convert.ToDouble(VolumeItems[i].Volume), max);
            }

            YAxisCoordinateMaxValue = (1 + YAxisValueRangeAndDataPeakScale) * max;
            YAxisCoordinateMinValue = 0;
        }

        public override void ScrollLeft()
        {
            if (VisualRrangeDataWidthInXAxisActualWidthProportion > 0.6)
            {
                MoveLeftCmd?.Execute(null);
            }
        }

        public override void ScrollRight()
        {
            MoveRightCmd?.Execute(null);
        }

        public override void ZoomIn()
        {
            if (VolumeStickWidth < XAxisActualWidth - 4 && VolumeStickWidth <= 24)
            {
                ZoomInCmd?.Execute(null);
            }
        }

        public override void ZoomOut()
        {
            if (VolumeStickWidth > 1.5)
            {
                ZoomOutCmd?.Execute(null);
            }
        }

        VolumeData IHasData<VolumeData>.GetData()
        {
            return new VolumeData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                DataMargin = VolumeStickMargin,
                DataWidth = VolumeStickWidth,
                VisualRangeEndIndex = VolumeViewAreaEndIndex,
                VisualRangeStartIndex = VolumeViewAreaStartIndex,
                ActualHeight = ActualHeight,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                YAxisCoordinateAndPixelProportion = YAxisCoordinateAndPixelProportion,
                Margin = Margin,
                DataSources = VolumeItems,
                FallBrush = FallBrush,
                RiseBrush = RiseBrush
            };
        }

        public void DrawNewest()
        {
            ComputeNewestVisualRrangeValues();
            Draw();
        }

        public void ComputeNewestVisualRrangeValues()
        {
            if (VolumeItems == null)
            {
                return;
            }

            var pointWidth = VolumeStickWidth + VolumeStickMargin * 2;
            var pointCount = (int)(XAxisActualWidth / pointWidth) - 2;
            VolumeViewAreaStartIndex = VolumeItems.Count - pointCount;
        }

        public void Clear()
        {
            VolumeItems = null;
        }

        AxisLineData IHasData<AxisLineData>.GetData()
        {
            return new AxisLineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualHeight = ActualHeight,
                ActualWidth = ActualWidth,
                Margin = Margin,
                YAxisCoordinateCount = YAxisCoordinateCount,
                AxisBrush = AxisBrush,
                ReferenceLineBrush = ReferenceLineBrush
            };
        }

        AxisLableData IHasData<AxisLableData>.GetData()
        {
            return new AxisLableData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualWidth = ActualWidth,
                Brush = AxisLabelBrush,
                YAxisActualHeight = YAxisActualHeight,
                YAxisCoordinateCount = YAxisCoordinateCount,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                YAxisValueRange = YAxisValueRange,
                FontSize = FontSize,
                Margin = Margin,
                Typeface = Typeface,
                TextFormat = TextFormat
            };
        }

        TimeLableData IHasData<TimeLableData>.GetData()
        {
            var dateTimes = new List<DateTime>();

            for (int i = VolumeViewAreaStartIndex; i < VolumeViewAreaEndIndex; i++)
            {
                dateTimes.Add(VolumeItems[i].Time);
            }

            return new TimeLableData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualHeight = ActualHeight,
                Brush = AxisLabelBrush,
                XAxisActualWidth = XAxisActualWidth,
                DataMargin = VolumeStickMargin,
                DataWidth = VolumeStickWidth,
                Margin = Margin,
                StickWidth = VolumeStickWidth,
                DateTimes = dateTimes,
                FontSize = FontSize,
                Typeface = Typeface,
                DateBrush = DateBrush,
                TimeSpan = TimeSpan
            };
        }

        LineData IHasData<LineData>.GetData()
        {
            var dataSource = new List<LineDataItem> { new LineDataItem { Brush = Brushes.White, Datas = Lines } };

            return new LineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualHeight = ActualHeight,
                DataSources = dataSource,
                YAxisCoordinateAndPixelProportion = YAxisCoordinateAndPixelProportion,
                VisualRangeEndIndex = VolumeViewAreaEndIndex,
                VisualRangeStartIndex = VolumeViewAreaStartIndex,
                DataMargin = VolumeStickMargin,
                DataWidth = VolumeStickWidth,
                Margin = Margin,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue
            };
        }

        public void LinkageMoveLeft()
        {
            if (VisualRrangeDataWidthInXAxisActualWidthProportion > 0.6)
            {
                if (VolumeStickWidth >= 3)
                {
                    VolumeViewAreaStartIndex += 5;
                }
                else if (VolumeStickWidth >= 2)
                {
                    VolumeViewAreaStartIndex += 10;
                }
                else
                {
                    VolumeViewAreaStartIndex += 20;
                }
                Draw();
            }
        }

        public void LinkageMoveRight()
        {
            if (VolumeStickWidth >= 3)
            {
                VolumeViewAreaStartIndex -= 5;
            }
            else if (VolumeStickWidth >= 2)
            {
                VolumeViewAreaStartIndex -= 10;
            }
            else
            {
                VolumeViewAreaStartIndex -= 20;
            }
            Draw();
        }

        public void LinkageZoomIn()
        {
            if (VolumeStickWidth < XAxisActualWidth - 4 && VolumeStickWidth <= 24)
            {
                VolumeStickWidth *= 1.2;
                VolumeStickMargin *= 1.2;
                VolumeViewAreaStartIndex += 10;
                Draw();
            }
        }

        public void LinkageZoomOut()
        {
            if (VolumeStickWidth > 1.5)
            {
                VolumeStickWidth /= 1.2;
                VolumeStickMargin /= 1.2;
                VolumeViewAreaStartIndex -= 10;
                Draw();
            }
        }

        GuideLineData IHasData<GuideLineData>.GetData()
        {
            var dateTimes = new List<DateTime>();

            for (int i = VolumeViewAreaStartIndex; i <= VolumeViewAreaEndIndex; i++)
            {
                dateTimes.Add(VolumeItems[i].Time);
            }

            return new GuideLineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualHeight = ActualHeight,
                Brush = AxisLabelBrush,
                ActualWidth = ActualWidth,
                ViewAreaEndIndex = VolumeViewAreaEndIndex,
                DataMargin = VolumeStickMargin,
                DataWidth = VolumeStickWidth,
                Margin = Margin,
                StickWidth = VolumeStickWidth,
                FontSize = FontSize,
                Typeface = Typeface,
                ViewAreaStartIndex = VolumeViewAreaStartIndex,
                MousePoint = MousePoint,
                RectengelBrush = FallBrush,
                Times = dateTimes,
                TextBrush = TextBrush,
                YAxisValueRange = YAxisValueRange,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                TextFormat = TextFormat
            };
        }

        protected override void MouseMoving()
        {
            if (VolumeItems != null)
            {
                MouseMoveCmd?.Execute(ActualHeight);
            }
        }

        public void LinkageMove()
        {
            if (VolumeItems != null)
            {
                Draw();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (VolumeItems != null)
            {
                Draw();
            }
        }
    }
}
