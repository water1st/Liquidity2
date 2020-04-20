using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Interface;
using System;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Render
{
    public class LineRender : IRender
    {
        public int Stage => RenderStages.LINE;

        public string Name => nameof(LineRender);

        public void Render(IHasData data)
        {
            if (data is IHasLineData avgLineData)
            {
                var lineDatas = avgLineData.GetData();

                if (lineDatas != null)
                {
                    if (lineDatas.DataSources != null)
                    {
                        var margin = lineDatas.DataWidth + lineDatas.DataMargin * 2;
                        var visual = new DrawingVisual();
                        using (var context = visual.RenderOpen())
                        {
                            for (int i = 0; i <= lineDatas.VisualRangeEndIndex - lineDatas.VisualRangeStartIndex - 1; i++)
                            {
                                var index = i + lineDatas.VisualRangeStartIndex;
                                var x1 = lineDatas.Margin.Left + margin * i + lineDatas.DataWidth / 2;
                                var x2 = x1 + margin;

                                foreach (var line in lineDatas.DataSources)
                                {
                                    if (line.Datas == null || line.Brush == null)
                                        continue;

                                    var y1 = ChartHelper.ComputeYCoordinate(lineDatas.ActualHeight, lineDatas.YAxisCoordinateMinValue, lineDatas.YAxisCoordinateAndPixelProportion, lineDatas.Margin.Top, Convert.ToDouble(line.Datas[index]));
                                    var y2 = ChartHelper.ComputeYCoordinate(lineDatas.ActualHeight, lineDatas.YAxisCoordinateMinValue, lineDatas.YAxisCoordinateAndPixelProportion, lineDatas.Margin.Top, Convert.ToDouble(line.Datas[index + 1]));

                                    var point1 = new Point(x1, y1);
                                    var point2 = new Point(x2, y2);
                                    var pen = new Pen { Brush = line.Brush };
                                    if (point1.Y > 0 && point1.X > 0)
                                    {
                                        context.DrawLine(pen, point1, point2);
                                    }
                                }
                            }
                        }

                        lineDatas.UpdateVisual(Name, visual);
                    }
                    else
                    {
                        lineDatas.UpdateVisual(Name, null);
                    }
                }
            }
        }
    }
}
