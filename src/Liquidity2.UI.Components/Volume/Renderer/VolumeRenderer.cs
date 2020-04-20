using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Components.Volume.Interface;
using Liquidity2.UI.Components.Volume.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Volume.Renderer
{
    public class VolumeRenderer : IRender
    {
        public int Stage => VolumeStages.VOLUME;

        public string Name => nameof(VolumeRenderer);

        public void Render(IHasData data)
        {
            if (data is IHasVolumeData volume)
            {
                var volumeData = volume.GetData();

                if (volumeData.DataSources != null && volumeData.DataSources.Any())
                {
                    var volumeItems = volumeData.DataSources;
                    var visuals = new List<Visual>();
                    for (int i = 0; i <= volumeData.VisualRangeEndIndex - volumeData.VisualRangeStartIndex; i++)
                    {
                        var volumeItem = volumeItems[i + volumeData.VisualRangeStartIndex];
                        var visual = new DrawingVisual();

                        using (var context = visual.RenderOpen())
                        {
                            var LeftBottomPoint = new Point
                            {
                                X = i * (volumeData.DataWidth + volumeData.DataMargin * 2) + volumeData.Margin.Left + volumeData.DataMargin,
                                Y = volumeData.ActualHeight - volumeData.Margin.Bottom
                            };

                            var LeftTopPoint = new Point
                            {
                                X = LeftBottomPoint.X,
                                Y = volumeData.ActualHeight - Convert.ToDouble(volumeItem.Volume) * volumeData.YAxisCoordinateAndPixelProportion * 0.96 - volumeData.Margin.Bottom
                            };

                            var RightTopPoint = new Point
                            {
                                X = LeftBottomPoint.X + volumeData.DataWidth,
                                Y = LeftTopPoint.Y
                            };

                            var RightBottomPoint = new Point
                            {
                                X = RightTopPoint.X,
                                Y = LeftBottomPoint.Y
                            };

                            var brush = volumeItem.Open < volumeItem.Close ? volumeData.RiseBrush : volumeData.FallBrush;

                            context.DrawRectangle(brush, null, new Rect(LeftBottomPoint, RightTopPoint));
                            visuals.Add(visual);
                        }
                    }
                    volumeData.UpdateVisuals(nameof(VolumeRenderer), visuals);
                }
                else
                {
                    volumeData.UpdateVisual(nameof(VolumeRenderer), null);
                }
            }
        }
    }
}
