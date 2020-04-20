namespace Liquidity2.UI.Components.Chart
{
    public static class ChartHelper
    {
        /// <summary>
        /// 通过Y轴值计算Y轴的像素坐标
        /// </summary>
        /// <param name="actualHeight">当前控件总高度</param>
        /// <param name="yAxisCoordinateMinValue">y轴最小值</param>
        /// <param name="yAxisCoordinateAndPixelProportion">像素与Y轴值比例（每像素可表达Y轴值比例）</param>
        /// <param name="topMargin">可视数据与控件顶部间隔</param>
        /// <param name="currentValue">y轴当前值</param>
        /// <returns>y轴坐标（像素）</returns>
        public static double ComputeYCoordinate(double actualHeight, double yAxisCoordinateMinValue, double yAxisCoordinateAndPixelProportion, double topMargin, double currentValue)
            => actualHeight - (currentValue - yAxisCoordinateMinValue) * yAxisCoordinateAndPixelProportion - topMargin;
    }
}
