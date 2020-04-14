using System.Windows.Input;

namespace Liquidity2.UI.Components.CustomCommand
{
    public interface IInitializationCommand
    {
        public ICommand InitializationCmd { get; set; }
    }
}
