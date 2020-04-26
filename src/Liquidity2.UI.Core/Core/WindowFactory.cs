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

        public object Create(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsSubclassOf(typeof(Window)))
            {
                throw new ArgumentException($"'{type.FullName}' is not subclass of Window");
            }

            var window = _provider.GetService(type);
            return window;
        }
    }
}
