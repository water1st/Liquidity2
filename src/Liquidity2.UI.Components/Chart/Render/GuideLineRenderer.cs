using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Interface;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Render
{
    public class GuideLineRenderer : IRender
    {
        public int Stage => RenderStages.Guide_Line;

        public string Name => nameof(GuideLineRenderer);

        public void Render(IHasData data)
        {
            if (data is IHasGuideLineData guideLineData)
            {
                var lineData = guideLineData.GetData();
                var visual = new DrawingVisual();

                using (var context = visual.RenderOpen())
                {
                    var pen = new Pen { Brush = lineData.Brush };
                    pen.DashStyle = DashStyles.Dash;

                    int index = Convert.ToInt32(Math.Floor((lineData.MousePoint.X - lineData.DataMargin - lineData.Margin.Left) / (lineData.DataWidth + lineData.DataMargin * 2)));

                    if (0 <= index && index <= (lineData.ViewAreaEndIndex - lineData.ViewAreaStartIndex))
                    {
                        var XAxisStartPoint = new Point
                        {
                            X = lineData.Margin.Left,
                            Y = lineData.MousePoint.Y
                        };

                        var XAxisEndPoint = new Point
                        {
                            X = lineData.ActualWidth - lineData.Margin.Right,
                            Y = lineData.MousePoint.Y
                        };

                        var YAxisStartPoint = new Point
                        {
                            X = index * (lineData.DataWidth + lineData.DataMargin * 2) + lineData.Margin.Left + lineData.DataMargin + lineData.DataWidth / 2,
                            Y = 0
                        };

                        var YAxisEndPoint = new Point
                        {
                            X = index * (lineData.DataWidth + lineData.DataMargin * 2) + lineData.Margin.Left + lineData.DataMargin + lineData.DataWidth / 2,
                            Y = lineData.ActualHeight - lineData.Margin.Bottom
                        };

                        var YActualHeight = lineData.ActualHeight - lineData.Margin.Bottom;
                        var PriceValue = ((YActualHeight - lineData.MousePoint.Y) / YActualHeight) * lineData.YAxisValueRange + lineData.YAxisCoordinateMinValue;

                        //画价格的文字
                        var priceText = new FormattedText(PriceValue.ToString(lineData.TextFormat), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, lineData.Typeface, lineData.FontSize, lineData.TextBrush, 1.25);

                        var PriceRectangleStartLeftTopPoint = new Point()
                        {
                            X = XAxisEndPoint.X,
                            Y = XAxisEndPoint.Y - priceText.Height / 2
                        };

                        var PriceTextPoint = new Point()
                        {
                            X = PriceRectangleStartLeftTopPoint.X + 7.5,
                            Y = PriceRectangleStartLeftTopPoint.Y
                        };

                        if (lineData.MousePoint.X > 0 && lineData.MousePoint.Y > 0 && lineData.MousePoint.Y < lineData.ActualHeight - lineData.Margin.Bottom)
                        {
                            context.DrawLine(pen, XAxisStartPoint, XAxisEndPoint);
                            context.DrawLine(pen, YAxisStartPoint, YAxisEndPoint);
                            context.DrawRectangle(lineData.RectengelBrush, new Pen() { Brush = lineData.RectengelBrush }, new Rect(PriceRectangleStartLeftTopPoint, new Size { Height = priceText.Height + 5, Width = priceText.Width + 15 }));
                            context.DrawText(priceText, PriceTextPoint);
                        }
                        else if (lineData.MousePoint.X > 0)
                        {
                            context.DrawLine(pen, YAxisStartPoint, YAxisEndPoint);
                        }

                        if (lineData.Times != null)
                        {
                            //画时间的文字
                            var timeText = new FormattedText(lineData.Times[index].ToString("M月dd日 HH:mm"), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, lineData.Typeface, lineData.FontSize, lineData.TextBrush, 1.25);

                            var TimeRectangleStartLeftTopPoint = new Point()
                            {
                                X = index * (lineData.DataWidth + lineData.DataMargin * 2) + lineData.Margin.Left + lineData.DataMargin + lineData.DataWidth - timeText.Width / 2 - 5,
                                Y = lineData.ActualHeight - lineData.Margin.Bottom
                            };

                            var TimeTextPoint = new Point()
                            {
                                X = TimeRectangleStartLeftTopPoint.X + 2.5,
                                Y = TimeRectangleStartLeftTopPoint.Y + 2.5
                            };

                            context.DrawRectangle(lineData.RectengelBrush, new Pen() { Brush = lineData.RectengelBrush }, new Rect(TimeRectangleStartLeftTopPoint, new Size { Height = timeText.Height + 5, Width = timeText.Width + 5 }));
                            context.DrawText(timeText, TimeTextPoint);
                        }
                    }
                    lineData.UpdateVisual(nameof(GuideLineRenderer), visual);
                }
            }
        }
    }
}
