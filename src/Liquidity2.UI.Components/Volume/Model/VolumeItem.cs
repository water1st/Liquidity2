using System;

namespace Liquidity2.UI.Components.Volume.Model
{
    public class VolumeItem
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// 交易量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
