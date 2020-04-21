using System.Threading.Tasks;

namespace Liquidity2.UI.Services.SelfSelect
{
    public interface ISelfSelectService
    {
        Task SubscribeTickerData();
        Task GetAllTickers();
        /// <summary>
        /// 获取自选币对
        /// </summary>
        /// <returns></returns>
        Task GetMarkSymbol();

        /// <summary>
        /// 增加自选币对
        /// </summary>
        /// <returns></returns>
        Task AddMarkSymbol(string symbol);

        /// <summary>
        /// 删除自选币对
        /// </summary>
        /// <returns></returns>
        Task RemoveMarkSymbol(string symbol);
    }
}
