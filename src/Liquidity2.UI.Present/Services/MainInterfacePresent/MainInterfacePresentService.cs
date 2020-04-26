using Liquidity2.Extensions.Lifecycle.Application;
using System.Threading;
using System.Threading.Tasks;

namespace Liquidity2.UI.Services
{
    public class MainInterfacePresentService : UIServiceStageObject
    {
        private readonly IWindowPresentService _windowPresent;

        public MainInterfacePresentService(IWindowPresentService windowPresent)
        {
            _windowPresent = windowPresent;
        }

        protected override Task OnStart(CancellationToken token)
        {
            return Present();
        }

        /// <summary>
        /// 加载UI设置并展示
        /// </summary>
        private async Task Present()
        {
            //加载所有窗体设置
            //如：定位、本地化
            //LoadUISetting()...

           await _windowPresent.ShowXXWindow();
        }
    }
}
