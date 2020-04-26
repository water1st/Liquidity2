using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.KLine.Interface;
using Liquidity2.UI.Components.KLine.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.KLine.Renderer
{
    public class KlineInfoRender : IRender
    {
        public int Stage => KLineStages.KLINE_INFO;

        public string Name => nameof(KlineInfoRender);

        public void Render(IHasData data)
        {
            if (data is IHasKlineInfoData klineInfoData)
            {
                var klineInfo = klineInfoData.GetData();
                if (klineInfo.Render)
                {
                    var point = new Point();

                    var visual = new DrawingVisual();
                    using (var context = visual.RenderOpen())
                    {

                        List<KlineInfo> texts = new List<KlineInfo>
                        {
                            new KlineInfo{Title="时间：" ,Info =$"{klineInfo.OHLC.Time:yyyy-MM-dd HH:mm}" },
                            new KlineInfo{Title="开=",Info=$"{klineInfo.OHLC.Open}"},
                            new KlineInfo{Title="高=",Info=$"{klineInfo.OHLC.Height}" },
                            new KlineInfo{Title="低=",Info=$"{klineInfo.OHLC.Low}"},
                            new KlineInfo{Title="收=",Info=$"{klineInfo.OHLC.Low}"},
                            new KlineInfo { Title = "成交量=", Info = $"{klineInfo.OHLC.Volume:F2}" }
                        };

                        var culture = CultureInfo.GetCultureInfo("en-us");
                        const int padding = 10;
                        double lineWidth = 0;

                        for (int i = 0; i < texts.Count; i++)
                        {
                            var titlePoint = new Point
                            {
                                X = point.X + padding * (i + 1) + lineWidth,
                                Y = point.Y
                            };
                            var text = texts[i];
                            var title = new FormattedText(text.Title, culture, FlowDirection.LeftToRight, klineInfo.Typeface, klineInfo.FontSize, klineInfo.Brush, 1.25);
                            context.DrawText(title, titlePoint);

                            lineWidth += title.Width;

                            var textPoint = new Point
                            {
                                X = point.X + padding * (i + 1) + lineWidth,
                                Y = point.Y
                            };

                            var textBrush = klineInfo.OHLC.Close <= klineInfo.OHLC.Open ? klineInfo.FallBrush : klineInfo.RiseBrush;

                            var info = new FormattedText(text.Info, culture, FlowDirection.LeftToRight, klineInfo.Typeface, klineInfo.FontSize, textBrush, 1.25);
                            context.DrawText(info, textPoint);

                            lineWidth += info.Width;
                        }
                    }
                    klineInfo.UpdateVisual(Name, visual);
                }
                else
                {
                    klineInfo.UpdateVisual(Name, null);
                }
            }
        }
    }
}
