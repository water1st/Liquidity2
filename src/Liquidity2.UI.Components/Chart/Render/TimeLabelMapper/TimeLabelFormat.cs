using System.Collections.Generic;

namespace Liquidity2.UI.Components.Chart.Render.TimeLabel
{
    public class TimeLabelFormat
    {
        public TimeLabelFormat(string format, IEnumerable<int> timeIndexs)
        {
            Format = format;
            TimeIndexs = timeIndexs;
        }

        public string Format { get; set; }

        public IEnumerable<int> TimeIndexs { get; set; }
    }
}
