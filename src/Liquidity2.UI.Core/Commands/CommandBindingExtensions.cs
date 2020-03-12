using System.Windows.Input;

namespace System.Windows
{
    public static class CommandBindingExtensions
    {
        public static void AddCommandBinding(this UIElement uIElement,
            ICommand command,
            CanExecuteRoutedEventHandler canExecute,
            ExecutedRoutedEventHandler executed)
        {
            var binding = new CommandBinding
            {
                Command = command
            };
            binding.CanExecute += canExecute;
            binding.Executed += executed;

            uIElement.CommandBindings.Add(binding);
        }
    }
}
