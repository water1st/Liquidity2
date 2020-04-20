using Liquidity2.UI.Components.Chart.Render;
using Liquidity2.UI.Components.Interface;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Renderer
{
    public class AxisLabelRenderer : IRender
    {
        public int Stage => RenderStages.AXIS_LABLE;

        public string Name => nameof(AxisLabelRenderer);

        public void Render(IHasData data)
        {
            if (data is IHasAxisLableData axisData)
            {

                var lableData = axisData.GetData();
                var visual = new DrawingVisual();
                using (var context = visual.RenderOpen())
                {
                    var proportion = 1 / lableData.YAxisCoordinateCount;
                    for (int i = 0; i < lableData.YAxisCoordinateCount; i++)
                    {
                        var currentValue = (i + 1) * proportion * lableData.YAxisValueRange + lableData.YAxisCoordinateMinValue;
                        var textPoint = new Point
                        {
                            X = lableData.ActualWidth - lableData.Margin.Right + 15,
                            Y = (lableData.YAxisCoordinateCount - i - 1) * proportion * lableData.YAxisActualHeight + lableData.Margin.Bottom - lableData.FontSize / 2
                        };
                        var text = new FormattedText(currentValue.ToString(lableData.TextFormat), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, lableData.Typeface, lableData.FontSize, lableData.Brush, 1.25);
                        context.DrawText(text, textPoint);
                    }
                }
                lableData.UpdateVisual(nameof(AxisLabelRenderer), visual);
            }
        }
    }
}
