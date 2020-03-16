using Liquidity2.Extensions.EventBus.EventObserver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Liquidity2.UI.Core.Core
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider provider;
        private readonly IEventBusRegistrator registrator;

        public WindowFactory(IServiceProvider provider, IEventBusRegistrator registrator)
        {
            this.provider = provider;
            this.registrator = registrator;
        }

        public TWindow Create<TWindow>() where TWindow : Window
        {
            var window = provider.GetService<TWindow>();
            if (window is IEventObserver observer)
            {
                observer.Subscribe(registrator);
            }

            return window;
        }
    }
}
