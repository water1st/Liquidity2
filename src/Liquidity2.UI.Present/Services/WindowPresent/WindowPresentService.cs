using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.Extensions.WindowPostions;
using Liquidity2.UI.Core;
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

        public Task ShowLoginWindow()
        {
            throw new NotImplementedException();
        }

        public Task ShowXXWindow()
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
