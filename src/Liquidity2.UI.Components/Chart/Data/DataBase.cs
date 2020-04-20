using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Model
{
    public abstract class DataBase
    {
        public Action<string, Visual> UpdateVisual { get; set; }
        public Action<string,IEnumerable<Visual>> UpdateVisuals { get; set; }
    }
}
