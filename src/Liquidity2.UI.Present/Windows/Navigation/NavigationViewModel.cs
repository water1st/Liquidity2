using Liquidity2.UI.Core;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Liquidity2.UI.Windows
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IWindowCommonBehavior _windowCommonBehavior;

        public ICommand WindowDragMoveCmd => _windowCommonBehavior.WindowDragMoveCmd;

        public ICommand AssetButtonClickCmd { get; } = new CustomRoutedCommand(nameof(AssetButtonClickCmd), typeof(NavigationWindow));

        public ICommand TosButtonClickCmd { get; } = new CustomRoutedCommand(nameof(TosButtonClickCmd), typeof(NavigationWindow));

        public ICommand KLineButtonClickCmd { get; } = new CustomRoutedCommand(nameof(KLineButtonClickCmd), typeof(NavigationWindow));

        public ICommand OrderButtonClickCmd { get; } = new CustomRoutedCommand(nameof(OrderButtonClickCmd), typeof(NavigationWindow));

        public ICommand EntrustButtonClickCmd { get; } = new CustomRoutedCommand(nameof(EntrustButtonClickCmd), typeof(NavigationWindow));

        public ICommand SelfSelectButtonClickCmd { get; } = new CustomRoutedCommand(nameof(SelfSelectButtonClickCmd), typeof(NavigationWindow));

        public ICommand AccountButtonClickCmd { get; } = new CustomRoutedCommand(nameof(AccountButtonClickCmd), typeof(NavigationWindow));

        public ICommand ExitButtonClickCmd { get; } = new CustomRoutedCommand(nameof(ExitButtonClickCmd), typeof(NavigationWindow));

        public ICommand InstallButtonClickCmd { get; } = new CustomRoutedCommand(nameof(InstallButtonClickCmd), typeof(NavigationWindow));

        public ICommand ErrorButtonClickCmd { get; } = new CustomRoutedCommand(nameof(ErrorButtonClickCmd), typeof(NavigationWindow));

        private string _account;
        public string Account
        {
            get => _account;
            set
            {
                _account = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Account)));
            }
        }

        private BitmapImage _portrait;
        public BitmapImage Portrait
        {
            get => _portrait;
            set
            {
                _portrait = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Portrait)));
            }
        }

        public NavigationViewModel(IWindowCommonBehavior windowCommonBehavior)
        {
            _windowCommonBehavior = windowCommonBehavior;
        }
    }
}
