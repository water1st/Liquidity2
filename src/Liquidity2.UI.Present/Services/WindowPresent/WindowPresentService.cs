using Liquidity2.Extensions.WindowPostions;
using Liquidity2.UI.Core;
using Liquidity2.UI.Windows;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Liquidity2.UI.Services
{
    public class WindowPresentService : IWindowPresentService
    {
        private readonly IWindowFactory windowFactory;
        private readonly IWindowPostionsService postionsService;

        public WindowPresentService(IWindowFactory windowFactory,
            IWindowPostionsService postionsService)
        {
            this.windowFactory = windowFactory;
            this.postionsService = postionsService;
        }

        public Task ShowAccountWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowAssetWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowEntrustWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowErrorWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowKLineWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowLoginWindow()
        {
            return ShowWindow<LoginWindow>();
        }

        public Task ShowNavigationWindow()
        {
            return ShowWindow<NavigationWindow>();
        }

        public Task ShowOrderWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowSelfSelectWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowSettingWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowTosWindow()
        {
            throw new NotImplementedException();
        }

        private async Task ShowWindow<TWindow>() where TWindow : Window
        {
            var window = windowFactory.Create<TWindow>();
            //设置窗体位置
            var defaultPostion = true;
            if (window is IWindowPostion windowPostion)
            {
                var postion = await postionsService.GetFirstPostionInQueue<TWindow>();
                if (postion != null)
                {
                    windowPostion.Postion = postion;
                    defaultPostion = false;
                }
            }

            //加载窗体样式
            if (window is ITemplateLoader templateLoader)
            {
                templateLoader.LoadeTemplate();
            }

            if (defaultPostion)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            window.Show();

        }
    }
}
