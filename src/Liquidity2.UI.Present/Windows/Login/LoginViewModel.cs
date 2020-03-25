using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Windows
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private Color _borderColor;
        private string _errorMessage;
        private string _passwordText;
        private string _accountText;
        private bool _rememberAccount;
        private bool _readTermsOfUse;
        private Visibility textBoxVisibility;
        private Visibility passwordBoxVisibility;
        private Brush showPasswordButtonBrush;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WindowDragMoveCmd { get; set; }

        public ICommand WindowMinimizeCmd { get; set; }

        public ICommand WindowCloseCmd { get; set; }

        public ICommand ShowPasswordButtonClickCmd { get; set; }

        public ICommand LoginButtonClickCmd { get; set; }

        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BorderColor)));
            }
        }

        public string AccountText
        {
            get => _accountText;
            set
            {
                _accountText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountText)));
            }
        }

        public string PasswordText
        {
            get => _passwordText;
            set
            {
                _passwordText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PasswordText)));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
            }
        }

        public Visibility TextBoxVisibility
        {
            get => textBoxVisibility; set
            {
                textBoxVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBoxVisibility)));
            }
        }

        public Visibility PasswordBoxVisibility
        {
            get => passwordBoxVisibility; set
            {
                passwordBoxVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PasswordBoxVisibility)));
            }
        }

        public Brush ShowPasswordButtonBrush
        {
            get => showPasswordButtonBrush; set
            {
                showPasswordButtonBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowPasswordButtonBrush)));
            }
        }

        public bool RememberAccount
        {
            get => _rememberAccount;
            set
            {
                _rememberAccount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RememberAccount)));

            }
        }

        public bool ReadTermsOfUse
        {
            get => _readTermsOfUse;
            set
            {
                _readTermsOfUse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReadTermsOfUse)));
            }
        }
    }
}
