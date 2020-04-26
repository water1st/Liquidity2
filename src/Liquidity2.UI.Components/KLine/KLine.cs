using Liquidity2.UI.Components.Chart;
using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine.Interface;
using Liquidity2.UI.Components.KLine.Model;
using Liquidity2.UI.Components.KLine.Renderer;
using Liquidity2.UI.Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Components.KLine
{
    public class KLine : ChartBase<IKLineChart>,
        IKLineChart,
        IHasAxisLineData,
        IHasAxisLableData,
        IHasKLineData,
        IHasGuideLineData,
        IHasLineData,
        IHasKlineInfoData
    {
        public static readonly DependencyProperty AxisBrushProperty = DependencyProperty.Register(nameof(AxisBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty AxisLabelBrushProperty = DependencyProperty.Register(nameof(AxisLabelBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty ReferenceLineBrushProperty = DependencyProperty.Register(nameof(ReferenceLineBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty RiseBrushProperty = DependencyProperty.Register(nameof(RiseBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty FallBrushProperty = DependencyProperty.Register(nameof(FallBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty KlineInfoBrushProperty = DependencyProperty.Register(nameof(KlineInfoBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty KlineInfoBorderBrushProperty = DependencyProperty.Register(nameof(KlineInfoBorderBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty KlineTextBrushProperty = DependencyProperty.Register(nameof(KlineTextBrush), typeof(Brush), typeof(KLine));
        public static readonly DependencyProperty KlineInfoTypefaceProperty = DependencyProperty.Register(nameof(KlineInfoTypeface), typeof(Typeface), typeof(KLine));
        public static readonly DependencyProperty TypefaceProperty = DependencyProperty.Register(nameof(Typeface), typeof(Typeface), typeof(KLine));
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(nameof(FontSize), typeof(double), typeof(KLine));
        public static readonly DependencyProperty KlineInfoFontSizeProperty = DependencyProperty.Register(nameof(KlineInfoFontSize), typeof(double), typeof(KLine));
        public static readonly DependencyProperty CandlestickWidthProperty = DependencyProperty.Register(nameof(CandlestickWidth), typeof(double), typeof(KLine));
        public static readonly DependencyProperty CandlestickMarginProperty = DependencyProperty.Register(nameof(CandlestickMargin), typeof(double), typeof(KLine));
        public static readonly DependencyProperty KlineInfoWidthProperty = DependencyProperty.Register(nameof(KlineInfoWidth), typeof(double), typeof(KLine));
        public static readonly DependencyProperty KlineInfoHeightProperty = DependencyProperty.Register(nameof(KlineInfoHeight), typeof(double), typeof(KLine));
        public static readonly DependencyProperty YAxisCoordinateCountProperty = DependencyProperty.Register(nameof(YAxisCoordinateCount), typeof(double), typeof(KLine));
        public static readonly DependencyProperty KLineInfoXMarginProperty = DependencyProperty.Register(nameof(KLineInfoXMargin), typeof(double), typeof(KLine));
        public static readonly DependencyProperty KLineInfoYMarginProperty = DependencyProperty.Register(nameof(KLineInfoYMargin), typeof(double), typeof(KLine));
        public static readonly DependencyProperty YAxisValueRangeAndDataPeakScaleProperty = DependencyProperty.Register(nameof(YAxisValueRangeAndDataPeakScale), typeof(double), typeof(KLine));
        public static readonly DependencyProperty OHLCsProperty = DependencyProperty.Register(nameof(OHLCs), typeof(IList<OHLC>), typeof(KLine));
        public static readonly DependencyProperty WhiteLinesProperty = DependencyProperty.Register(nameof(Lines), typeof(IList<decimal>), typeof(KLine));

        public static readonly DependencyProperty SelectRenderProperty =
            DependencyProperty.Register(nameof(SelectRenders), typeof(IList<Type>), typeof(KLine));

        public static readonly DependencyProperty LinkageMoveLeftProperty = DependencyProperty.Register(nameof(MoveLeftCmd), typeof(ICommand), typeof(KLine));

        public static readonly DependencyProperty LinkageMoveRightProperty = DependencyProperty.Register(nameof(MoveRightCmd), typeof(ICommand), typeof(KLine));

        public static readonly DependencyProperty LinkageZoomInProperty = DependencyProperty.Register(nameof(ZoomInCmd), typeof(ICommand), typeof(KLine));

        public static readonly DependencyProperty LinkageZoomOutProperty = DependencyProperty.Register(nameof(ZoomOutCmd), typeof(ICommand), typeof(KLine));

        public static readonly DependencyProperty MouseMoveProperty = DependencyProperty.Register(nameof(MouseMoveCmd), typeof(ICommand), typeof(KLine));

        public static readonly DependencyProperty TextBrushProperty =
           DependencyProperty.Register(nameof(TextBrush), typeof(Brush), typeof(KLine));

        public string TextFormat
        {
            get => (string)GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
        }

        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register(nameof(TextFormat), typeof(string), typeof(KLine));

        private readonly IDictionary<Visual, KlineMappingData> _klineMapping;
        private KlineMappingData _hoverKlineMappingData;
        private int _kLineViewAreaStartIndex;

        public KLine()
        {
            _klineMapping = new Dictionary<Visual, KlineMappingData>();
        }

        public Brush TextBrush
        {
            get => (Brush)GetValue(TextBrushProperty);
            set => SetValue(TextBrushProperty, value);
        }

        public ICommand MoveLeftCmd
        {
            get => (ICommand)GetValue(LinkageMoveLeftProperty);
            set => SetValue(LinkageMoveLeftProperty, value);
        }

        public ICommand MoveRightCmd
        {
            get => (ICommand)GetValue(LinkageMoveRightProperty);
            set => SetValue(LinkageMoveRightProperty, value);
        }

        public ICommand ZoomInCmd
        {
            get => (ICommand)GetValue(LinkageZoomInProperty);
            set => SetValue(LinkageZoomInProperty, value);
        }

        public ICommand ZoomOutCmd
        {
            get => (ICommand)GetValue(LinkageZoomOutProperty);
            set => SetValue(LinkageZoomOutProperty, value);
        }


        public ICommand MouseMoveCmd
        {
            get => (ICommand)GetValue(MouseMoveProperty);
            set => SetValue(MouseMoveProperty, value);
        }

        public KLineTimeSpan TimeSpan { get; set; } = KLineTimeSpan.FiveMin;

        public IList<OHLC> OHLCs
        {
            get => (IList<OHLC>)GetValue(OHLCsProperty);
            set => SetValue(OHLCsProperty, value);
        }

        public IList<decimal> Lines
        {
            get => (IList<decimal>)GetValue(WhiteLinesProperty);
            set => SetValue(WhiteLinesProperty, value);
        }

        public double YAxisCoordinateCount
        {
            get => (double)GetValue(YAxisCoordinateCountProperty);
            set => SetValue(YAxisCoordinateCountProperty, value);
        }

        public double KLineInfoXMargin
        {
            get => (double)GetValue(KLineInfoXMarginProperty);
            set => SetValue(KLineInfoXMarginProperty, value);
        }

        public double KLineInfoYMargin
        {
            get => (double)GetValue(KLineInfoYMarginProperty);
            set => SetValue(KLineInfoYMarginProperty, value);
        }

        public double YAxisCoordinateMaxValue { get; private set; }

        public double YAxisCoordinateMinValue { get; private set; }

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

        public double KlineInfoWidth
        {
            get => (double)GetValue(KlineInfoWidthProperty);
            set => SetValue(KlineInfoWidthProperty, value);
        }

        public double KlineInfoHeight
        {
            get => (double)GetValue(KlineInfoHeightProperty);
            set => SetValue(KlineInfoHeightProperty, value);
        }

        public double KlineInfoFontSize
        {
            get => (double)GetValue(KlineInfoFontSizeProperty);
            set => SetValue(KlineInfoFontSizeProperty, value);
        }

        public Brush KlineInfoBrush
        {
            get => (Brush)GetValue(KlineInfoBrushProperty);
            set => SetValue(KlineInfoBrushProperty, value);
        }

        public Brush KlineInfoBorderBrush
        {
            get => (Brush)GetValue(KlineInfoBorderBrushProperty);
            set => SetValue(KlineInfoBorderBrushProperty, value);
        }

        public Brush KlineTextBrush
        {
            get => (Brush)GetValue(KlineTextBrushProperty);
            set => SetValue(KlineTextBrushProperty, value);
        }

        public Typeface KlineInfoTypeface
        {
            get => (Typeface)GetValue(KlineInfoTypefaceProperty);
            set => SetValue(KlineInfoTypefaceProperty, value);
        }

        public int KLineViewAreaStartIndex
        {
            get => _kLineViewAreaStartIndex;
            private set
            {
                if (OHLCs != null)
                {
                    _kLineViewAreaStartIndex = value <= 0 ? 0 : value;

                    if (value >= OHLCs.Count - 10)
                    {
                        _kLineViewAreaStartIndex = OHLCs.Count - 10;
                    }
                }
            }
        }

        public int KLineViewAreaEndIndex
        {
            get
            {
                if (OHLCs == null)
                {
                    return 0;
                }

                var pointWidth = CandlestickWidth + CandlestickMargin * 2;
                var pointCount = (int)(XAxisActualWidth / pointWidth) - 1;
                var result = KLineViewAreaStartIndex + pointCount;
                result = result > OHLCs.Count - 1 ? OHLCs.Count - 1 : result;
                return result;
            }
        }

        public double YAxisValueRangeAndDataPeakScale
        {
            get => (double)GetValue(YAxisValueRangeAndDataPeakScaleProperty);
            set => SetValue(YAxisValueRangeAndDataPeakScaleProperty, value);
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

        public double CandlestickWidth
        {
            get => (double)GetValue(CandlestickWidthProperty);
            set => SetValue(CandlestickWidthProperty, value);
        }

        public double CandlestickMargin
        {
            get => (double)GetValue(CandlestickMarginProperty);
            set => SetValue(CandlestickMarginProperty, value);
        }

        public IList<Type> SelectRenders
        {
            get => (IList<Type>)GetValue(SelectRenderProperty);
            set => SetValue(SelectRenderProperty, value);
        }

        public double XAxisActualWidth => ActualWidth - Margin.Left - Margin.Right;

        public double YAxisActualHeight => ActualHeight - Margin.Top - Margin.Bottom;

        public double YAxisValueRange => YAxisCoordinateMaxValue - YAxisCoordinateMinValue;

        public double YAxisCoordinateAndPixelProportion => YAxisActualHeight / YAxisValueRange;

        private Point MousePoint => Mouse.GetPosition(this);

        public double VisualRrangeDataWidth
        {
            get
            {
                if (OHLCs == null)
                {
                    return 0;
                }

                var pointCount = KLineViewAreaEndIndex - KLineViewAreaStartIndex;
                var width = pointCount * (CandlestickWidth + CandlestickMargin * 2);
                return width;
            }
        }

        public double VisualRrangeDataWidthInXAxisActualWidthProportion => VisualRrangeDataWidth / XAxisActualWidth;

        public double ViewActualHeight { get; set; }

        public override void Draw()
        {
            if (OHLCs == null)
            {
                return;
            }

            ComputeVisualRrangeValues();
            var kLine = Renders.ToList().Find(data => data.GetType() == typeof(KLineRenderer)) as KLineRenderer;
            kLine.AllowRender = TimeSpan != KLineTimeSpan.Time;
            foreach (IRender render in Renders)
            {
                if (SelectRenders.ToList().FindIndex(x => x == render.GetType()) != -1)
                {
                    render.Render(this);
                }
            }
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
            if ((CandlestickWidth < XAxisActualWidth - 4) && (CandlestickWidth <= 24))
            {
                ZoomInCmd?.Execute(null);
            }
        }

        public override void ZoomOut()
        {
            if (CandlestickWidth > 1.5)
            {
                ZoomOutCmd?.Execute(null);
            }
        }

        public void Clear()
        {
            OHLCs = null;
        }

        AxisLineData IHasData<AxisLineData>.GetData()
        {
            return new AxisLineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualWidth = ActualWidth,
                ActualHeight = ActualHeight,
                Margin = Margin,
                YAxisCoordinateCount = YAxisCoordinateCount,
                AxisBrush = AxisBrush,
                ReferenceLineBrush = ReferenceLineBrush,
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

        LineData IHasData<LineData>.GetData()
        {

            var dataSource = new List<LineDataItem> { new LineDataItem { Brush = Brushes.White, Datas = Lines } };

            return new LineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                DataSources = dataSource,
                YAxisCoordinateAndPixelProportion = YAxisCoordinateAndPixelProportion,
                VisualRangeStartIndex = KLineViewAreaStartIndex,
                VisualRangeEndIndex = KLineViewAreaEndIndex,
                ActualHeight = ActualHeight,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                Margin = Margin,
                DataMargin = CandlestickMargin,
                DataWidth = CandlestickWidth,
            };
        }

        GuideLineData IHasData<GuideLineData>.GetData()
        {
            return new GuideLineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                ActualHeight = ActualHeight,
                Brush = AxisLabelBrush,
                ActualWidth = ActualWidth,
                ViewAreaEndIndex = KLineViewAreaEndIndex,
                DataMargin = CandlestickMargin,
                DataWidth = CandlestickWidth,
                Margin = Margin,
                StickWidth = CandlestickWidth,
                FontSize = FontSize,
                Typeface = Typeface,
                ViewAreaStartIndex = KLineViewAreaStartIndex,
                MousePoint = MousePoint,
                TextBrush = TextBrush,
                YAxisValueRange = YAxisValueRange,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                RectengelBrush = FallBrush,
                TextFormat = TextFormat
            };
        }

        KLineData IHasData<KLineData>.GetData()
        {
            _klineMapping.Clear();
            return new KLineData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals,
                DataMargin = CandlestickMargin,
                DataWidth = CandlestickWidth,
                VisualRangeEndIndex = KLineViewAreaEndIndex,
                VisualRangeStartIndex = KLineViewAreaStartIndex,
                ActualHeight = ActualHeight,
                YAxisCoordinateMinValue = YAxisCoordinateMinValue,
                FallBrush = FallBrush,
                YAxisCoordinateAndPixelProportion = YAxisCoordinateAndPixelProportion,
                Margin = Margin,
                DataSources = OHLCs,
                RiseBrush = RiseBrush,
                Mapping = _klineMapping
            };
        }

        KlineInfoData IHasData<KlineInfoData>.GetData()
        {
            var result = new KlineInfoData
            {
                UpdateVisual = UpdateVisual,
                UpdateVisuals = UpdateVisuals
            };

            int index = Convert.ToInt32(Math.Floor((MousePoint.X - CandlestickMargin - Margin.Left) / (CandlestickWidth + CandlestickMargin * 2)));

            if (0 <= index && index <= (KLineViewAreaEndIndex - KLineViewAreaStartIndex))
            {
                result.UpdateVisual = UpdateVisual;
                result.UpdateVisuals = UpdateVisuals;
                result.XAxisActualWidth = ActualWidth - Margin.Right;
                result.YAxisActualHeight = YAxisActualHeight;
                result.OHLC = OHLCs[KLineViewAreaStartIndex + index];
                result.BorderBrush = KlineInfoBorderBrush;
                result.Brush = KlineInfoBrush;
                result.RiseBrush = RiseBrush;
                result.FallBrush = FallBrush;
                result.Typeface = KlineInfoTypeface;
                result.Height = KlineInfoHeight;
                result.Width = KlineInfoHeight;
                result.Width = KlineInfoWidth;
                result.FontSize = KlineInfoFontSize;
                result.XMargin = KLineInfoXMargin;
                result.YMargin = KLineInfoYMargin;
                result.Render = true;
            }

            return result;
        }

        private void ComputeVisualRrangeValues()
        {
            if (OHLCs == null)
            {
                return;
            }

            var max = double.MinValue;
            var min = double.MaxValue;
            for (int i = KLineViewAreaStartIndex; i <= KLineViewAreaEndIndex; i++)
            {
                max = Math.Max(Convert.ToDouble(OHLCs[i].Height), max);
                min = Math.Min(Convert.ToDouble(OHLCs[i].Low), min);
            }

            YAxisCoordinateMaxValue = (1 + YAxisValueRangeAndDataPeakScale) * max;
            YAxisCoordinateMinValue = (1 - YAxisValueRangeAndDataPeakScale) * min;
        }

        private void ComputeNewestVisualRrangeValues()
        {
            if (OHLCs == null)
            {
                return;
            }

            var pointWidth = CandlestickWidth + CandlestickMargin * 2;
            var pointCount = (int)(XAxisActualWidth / pointWidth) - 2;
            KLineViewAreaStartIndex = OHLCs.Count - pointCount;
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            Proxy.SetProxy(this);
        }

        protected override HitTestResultBehavior VisualHitCallback(HitTestResult result)
        {
            if (result.VisualHit is DrawingVisual visual && visual != null && _klineMapping.ContainsKey(visual))
            {
                _hoverKlineMappingData = _klineMapping[visual];
                Draw();
            }
            else
            {
                _hoverKlineMappingData = null;
            }

            return HitTestResultBehavior.Stop;
        }

        public void DrawNewest()
        {
            ComputeNewestVisualRrangeValues();
            Draw();
        }

        public void LinkageMoveLeft()
        {
            if (VisualRrangeDataWidthInXAxisActualWidthProportion > 0.6)
            {
                if (CandlestickWidth >= 3)
                {
                    KLineViewAreaStartIndex += 5;
                }
                else if (CandlestickWidth >= 2)
                {
                    KLineViewAreaStartIndex += 10;
                }
                else
                {
                    KLineViewAreaStartIndex += 20;
                }
                Draw();
            }
        }

        public void LinkageMoveRight()
        {
            if (CandlestickWidth >= 3)
            {
                KLineViewAreaStartIndex -= 5;
            }
            else if (CandlestickWidth >= 2)
            {
                KLineViewAreaStartIndex -= 10;
            }
            else
            {
                KLineViewAreaStartIndex -= 20;
            }
            Draw();
        }

        public void LinkageZoomIn()
        {
            if ((CandlestickWidth < XAxisActualWidth - 4) && (CandlestickWidth <= 24))
            {
                CandlestickWidth *= 1.2;
                CandlestickMargin *= 1.2;
                KLineViewAreaStartIndex += 10;
                Draw();
            }
        }

        public void LinkageZoomOut()
        {
            if (CandlestickWidth > 1.5)
            {
                CandlestickWidth /= 1.2;
                CandlestickMargin /= 1.2;
                KLineViewAreaStartIndex -= 10;
                Draw();
            }
        }

        protected override void MouseMoving()
        {
            if (OHLCs != null)
            {
                MouseMoveCmd?.Execute(ActualHeight);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (OHLCs != null)
            {
                Draw();
            }
        }

        public void LinkageMove()
        {
            if (OHLCs != null)
            {
                Draw();
            }
        }
    }
}
