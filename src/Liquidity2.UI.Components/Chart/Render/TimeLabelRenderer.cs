using Liquidity2.UI.Components.Chart.Data;
using Liquidity2.UI.Components.Interface;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart.Render
{
    public class TimeLabelRenderer : IRender
    {
        public int Stage => RenderStages.TIME_LABLE;

        public string Name => nameof(TimeLabelRenderer);

        private ITimeLabelMapper timeLabelMapper;

        public TimeLabelRenderer(ITimeLabelMapper timeLabelMapper)
        {
            this.timeLabelMapper = timeLabelMapper;
        }

        public void Render(IHasData data)
        {
            if (data is IHasTimeLableData timeLableData)
            {
                var lableData = timeLableData.GetData();

                var labelFormat = timeLabelMapper.MapToFormat(lableData.TimeSpan, lableData.StickWidth, lableData.DateTimes);

                var visual = new DrawingVisual();
                using (var context = visual.RenderOpen())
                {
                    var pen = new Pen { Brush = lableData.Brush };

                    foreach (var index in labelFormat.TimeIndexs)
                    {
                        var format = labelFormat.Format;
                        var pointStart = new Point() { X = index * (lableData.DataWidth + lableData.DataMargin * 2) + lableData.Margin.Left + lableData.DataMargin + lableData.DataWidth / 2, Y = lableData.ActualHeight - lableData.Margin.Bottom };
                        var pointEnd = new Point()
                        {
                            X = pointStart.X,
                            Y = pointStart.Y + 5
                        };

                        //如果为0点则显示为天数
                        if (lableData.DateTimes[index].ToLocalTime().ToString("HH:mm:ss") == "00:00:00")
                        {
                            format = "M月d日";
                        }

                        var text = new FormattedText(lableData.DateTimes[index].ToString(format), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, lableData.Typeface, lableData.FontSize, lableData.Brush, 1.25);

                        var textPoint = new Point()
                        {
                            X = pointStart.X - text.Width / 2,
                            Y = pointEnd.Y
                        };

                        context.DrawLine(pen, pointStart, pointEnd);
                        context.DrawText(text, textPoint);
                    }
                }

                lableData.UpdateVisual(nameof(TimeLabelRenderer), visual);
            }
        }
    }
}
