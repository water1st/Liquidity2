using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Components.Windows
{
    public interface IInputWindow : IInputElement
    {
        void Show(WindowPosition BaseWindowPosition = null);
        void Close();
        ICommand InputCmd { get; set; }
    }       
}
