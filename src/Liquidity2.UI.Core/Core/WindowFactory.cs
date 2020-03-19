using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Liquidity2.UI.Core
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider _provider;

        public WindowFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public TWindow Create<TWindow>() where TWindow : Window
        {
            var window = _provider.GetService<TWindow>();
            return window;
        }
    }
}
