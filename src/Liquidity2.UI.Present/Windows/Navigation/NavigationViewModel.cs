using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Liquidity2.UI.Windows
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        private BitmapImage _portrait;

        public event PropertyChangedEventHandler PropertyChanged;

        public BitmapImage Portrait
        {
            get => _portrait;
            set
            {
                _portrait = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Portrait)));
            }
        }

        public ICommand WindowDragMoveCmd { get; set; }
        public ICommand AssetButtonClickCmd { get; set; }
        public ICommand TosButtonClickCmd { get; set; }
        public ICommand KLineButtonClickCmd { get; set; }
        public ICommand OrderButtonClickCmd { get; set; }
        public ICommand EntrustButtonClickCmd { get; set; }
        public ICommand SelfSelectButtonClickCmd { get; set; }
        public ICommand AccountButtonClickCmd { get; set; }
        public ICommand ExitButtonClickCmd { get; set; }
        public ICommand InstallButtonClickCmd { get; set; }
        public ICommand ErrorButtonClickCmd { get; set; }
        public string Account { get; set; }
        
    }
}
