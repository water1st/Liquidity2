using System.Windows.Input;

namespace Liquidity2.UI.Components.CustomCommand
{
    public interface IInputCommand
    {
        public ICommand InputCmd { get; set; }
    }
}
