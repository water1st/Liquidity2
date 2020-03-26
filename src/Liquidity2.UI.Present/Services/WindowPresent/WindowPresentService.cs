using Liquidity2.Extensions.EventBus.EventObserver;
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
        private readonly IWindowFactory _windowFactory;
        private readonly IWindowPostionsService _postionsService;
        private readonly IEventBusRegistrator _registrator;

        public WindowPresentService(IWindowFactory windowFactory,
            IWindowPostionsService postionsService, IEventBusRegistrator registrator)
        {
            _windowFactory = windowFactory;
            _postionsService = postionsService;
            _registrator = registrator;
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
            var window = _windowFactory.Create<LoginWindow>();
            return ShowWindow(window);
        }

        public Task ShowNavigationWindow()
        {
            var window = _windowFactory.Create<NavigationWindow>();
            return ShowWindow(window);
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

        private async Task ShowWindow<TWindow>(TWindow window) where TWindow : Window
        {
            //设置窗体位置
            var defaultPostion = true;
            if (window is IWindowPostion windowPostion)
            {
                var postion = await _postionsService.GetFirstPostionInQueue<TWindow>();
                if (postion != null)
                {
                    windowPostion.Postion = postion;
                    defaultPostion = false;
                }
            }

            if (window is IEventObserver observer)
            {
                observer.Subscribe(_registrator);
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
