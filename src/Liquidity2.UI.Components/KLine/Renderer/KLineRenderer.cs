using Liquidity2.UI.Components.Chart;
using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.KLine.Renderer
{
    public class KLineRenderer : IRender
    {
        public int Stage => KLineStages.KLINE;

        public string Name => nameof(KLineRenderer);

        public bool AlwaysReRender => true;

        /// <summary>
        /// 允许渲染
        /// </summary>
        public bool AllowRender { get; set; }

        public void Render(IHasData data)
        {
            if (data is IHasKLineData kline)
            {
                var kLineData = kline.GetData();

                if ((kLineData.DataSources != null && kLineData.DataSources.Any()) && AllowRender)
                {
                    var ohlcs = kLineData.DataSources;
                    var visuals = new List<Visual>();
                    for (int i = 0; i <= kLineData.VisualRangeEndIndex - kLineData.VisualRangeStartIndex; i++)
                    {
                        var ohlc = ohlcs[i + kLineData.VisualRangeStartIndex];
                        var visual = new DrawingVisual();
                        using (var context = visual.RenderOpen())
                        {
                            var openCloseMaxPoint = new Point
                            {
                                X = i * (kLineData.DataWidth + kLineData.DataMargin * 2) + kLineData.Margin.Left + kLineData.DataMargin,
                                Y = ChartHelper.ComputeYCoordinate(kLineData.ActualHeight, kLineData.YAxisCoordinateMinValue, kLineData.YAxisCoordinateAndPixelProportion, kLineData.Margin.Top, Convert.ToDouble(Math.Max(ohlc.Open, ohlc.Close)))
                            };
                            var openCloseMinPoint = new Point
                            {
                                X = i * (kLineData.DataWidth + kLineData.DataMargin * 2) + kLineData.Margin.Left + kLineData.DataMargin + kLineData.DataWidth,
                                Y = ChartHelper.ComputeYCoordinate(kLineData.ActualHeight, kLineData.YAxisCoordinateMinValue, kLineData.YAxisCoordinateAndPixelProportion, kLineData.Margin.Top, Convert.ToDouble(Math.Min(ohlc.Open, ohlc.Close)))
                            };
                            var midLineHeightPoint = new Point
                            {
                                X = openCloseMaxPoint.X + kLineData.DataWidth / 2,
                                Y = ChartHelper.ComputeYCoordinate(kLineData.ActualHeight, kLineData.YAxisCoordinateMinValue, kLineData.YAxisCoordinateAndPixelProportion, kLineData.Margin.Top, Convert.ToDouble(ohlc.Height))
                            };
                            var midLineLowPoint = new Point
                            {
                                X = openCloseMaxPoint.X + kLineData.DataWidth / 2,
                                Y = ChartHelper.ComputeYCoordinate(kLineData.ActualHeight, kLineData.YAxisCoordinateMinValue, kLineData.YAxisCoordinateAndPixelProportion, kLineData.Margin.Top, Convert.ToDouble(ohlc.Low))
                            };

                            //设置最小蜡烛宽度
                            if ((openCloseMinPoint.Y - openCloseMaxPoint.Y) < 3)
                            {
                                openCloseMinPoint.Y = openCloseMaxPoint.Y + 3;
                            }

                            var brush = ohlc.Open < ohlc.Close ? kLineData.RiseBrush : kLineData.FallBrush;
                            context.DrawRectangle(brush, null, new Rect(openCloseMaxPoint, openCloseMinPoint));
                            context.DrawLine(new Pen { Brush = brush, Thickness = kLineData.DataWidth / 8 }, midLineHeightPoint, midLineLowPoint);

                            visuals.Add(visual);
                            kLineData.Mapping.Add(visual, new Model.KlineMappingData { OHLC = ohlc, MidLineHeightPoint = midLineHeightPoint, MidLineLowPoint = midLineLowPoint });
                        }
                    }
                    kLineData.UpdateVisuals(nameof(KLineRenderer), visuals);
                }
                else
                {
                    kLineData.UpdateVisual(nameof(KLineRenderer), null);
                }
            }
        }
    }
}
