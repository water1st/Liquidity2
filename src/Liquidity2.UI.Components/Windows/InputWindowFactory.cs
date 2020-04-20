using Liquidity2.UI.Components.UsersControl.GroupButton;
using System;

namespace Liquidity2.UI.Components.Windows
{
    public class InputWindowFactory : IInputWindowFactory
    {
        private readonly IServiceProvider provider;

        public InputWindowFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IInputWindow Create(string className)
        {
            var type = GetWindowType(className);
            return provider.GetService(type) as IInputWindow;
        }

        private Type GetWindowType(string className)
        {
            if (className == nameof(GroupWindow))
                return typeof(GroupWindow);
            return null;
        }
    }
}
