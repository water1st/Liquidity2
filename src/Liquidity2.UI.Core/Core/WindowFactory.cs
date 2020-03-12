using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Liquidity2.UI.Core.Core
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider provider;

        public WindowFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public TWindow Create<TWindow>() where TWindow : Window
        {
            return provider.GetService<TWindow>();
        }
    }
}
