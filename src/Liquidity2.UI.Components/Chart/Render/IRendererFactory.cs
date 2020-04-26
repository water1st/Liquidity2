using System.Collections.Generic;

namespace Liquidity2.UI.Components.Interface
{
    public interface IRendererFactory
    {
        IEnumerable<IRender> Create();
    }
}
