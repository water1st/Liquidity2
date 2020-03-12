namespace System.Windows.Input
{
    public class CustomRoutedCommand : RoutedCommand, ICommand
    {
        private readonly IInputElement _defaultTarget;
        public CustomRoutedCommand(IInputElement defaultTarget = null)
        {
            _defaultTarget = defaultTarget;
        }

        public CustomRoutedCommand(string name, Type ownerType, IInputElement defaultTarget = null) : base(name, ownerType)
        {
            _defaultTarget = defaultTarget;
        }

        public CustomRoutedCommand(string name, Type ownerType, InputGestureCollection inputGestures, IInputElement defaultTarget = null) : base(name, ownerType, inputGestures)
        {
            _defaultTarget = defaultTarget;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter, _defaultTarget);
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter, _defaultTarget);
        }
    }
}
