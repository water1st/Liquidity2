using Liquidity2.UI.Components.Chart.Render;
using Liquidity2.UI.Components.Interface;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Renderer
{
    public class AxisLineRenderer : IRender
    {
        public int Stage => RenderStages.AXIS_LINE;

        public string Name => nameof(AxisLineRenderer);

        public void Render(IHasData data)
        {
            if (data is IHasAxisLineData axisData)
            {
                var axis = axisData.GetData();
                var visual = new DrawingVisual();
                using (var context = visual.RenderOpen())
                {
                    var pen = new Pen { Brush = axis.AxisBrush };
                    //画X轴
                    var axisXStartPoint = new Point { X = axis.Margin.Left, Y = axis.ActualHeight - axis.Margin.Bottom };
                    var axisXEndPoint = new Point { X = axis.ActualWidth - axis.Margin.Right, Y = axis.ActualHeight - axis.Margin.Bottom };
                    context.DrawLine(pen, axisXStartPoint, axisXEndPoint);
                    //画Y轴
                    var axisYStartPoint = new Point { X = axis.ActualWidth - axis.Margin.Right, Y = 0 };
                    var axisYEndPoint = new Point { X = axis.ActualWidth - axis.Margin.Right, Y = axis.ActualHeight - axis.Margin.Bottom };
                    context.DrawLine(pen, axisYStartPoint, axisYEndPoint);
                    var proportion = 1 / axis.YAxisCoordinateCount;
                    var linePen = new Pen { Brush = axis.ReferenceLineBrush };
                    for (int i = 0; i < axis.YAxisCoordinateCount; i++)
                    {
                        //坐标点的位置
                        //获取当前Y刻度位置是从可视区域底部开始开始向上画
                        //以Y轴最高值和Y轴最低值之间的差值乘以刻度比例再加上底部补偿值
                        var currentYPoint = (axis.YAxisCoordinateCount - i - 1) * proportion * axis.YAxisActualHeight + axis.Margin.Bottom;
                        var pointStart = new Point { X = axis.ActualWidth - axis.Margin.Right, Y = currentYPoint };
                        var pointEnd = new Point { X = pointStart.X + 10, Y = currentYPoint };
                        //参考线的位置
                        var linePointStart = new Point { X = axis.Margin.Left, Y = currentYPoint };
                        context.DrawLine(pen, pointStart, pointEnd);
                        context.DrawLine(linePen, linePointStart, pointStart);
                    }
                }
                axis.UpdateVisual(nameof(AxisLineRenderer), visual);
            }
        }
    }
}
