using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liquidity2.UI.Components.UsersControl
{
    public class DropdownMenu : UserControl
    {
        public ICommand InputCmd { get; } = new RoutedCommand(nameof(InputCmd), typeof(DropdownMenu));

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedProperty);

            set => SetValue(TextChangedProperty, value);
        }

        public static readonly DependencyProperty TextChangedProperty = DependencyProperty.Register(nameof(TextChangedCommand), typeof(ICommand), typeof(DropdownMenu));

        public DropdownMenu()
        {
            Loaded += Initialize;
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            this.AddCommandBinding(InputCmd, InputCb_CanExecute, InputCb_Executed);
        }

        private void InputCb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextChangedCommand?.Execute(e.Parameter);
        }

        private void InputCb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
