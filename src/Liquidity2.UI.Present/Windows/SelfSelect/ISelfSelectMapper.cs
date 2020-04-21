using Liquidity2.UI.Services.SelfSelect.DTO;
using Liquidity2.UI.Windows.SelfSelect.Model;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public interface ISelfSelectMapper
    {
        SelfSelectData MapToSelfSelectData(Ticker ticker);
    }
}
