using System.Threading.Tasks;

namespace Liquidity2.UI.Services
{
    public interface IWindowPresentService
    {
        Task ShowLoginWindow();
        Task ShowNavigationWindow();
        Task ShowErrorWindow();
        Task ShowSelfSelectWindow();
        Task ShowAccountWindow();
        Task ShowAssetWindow();
        Task ShowTosWindow();
        Task ShowKLineWindow();
        Task ShowOrderWindow();
        Task ShowSettingWindow();
        Task ShowEntrustWindow();
    }
}
