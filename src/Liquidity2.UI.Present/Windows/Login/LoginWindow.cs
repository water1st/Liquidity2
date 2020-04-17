using Liquidity2.Extensions.Authentication.Events;
using Liquidity2.Extensions.Authentication.Service;
using Liquidity2.Extensions.EventBus;
using Liquidity2.Extensions.EventBus.EventObserver;
using Liquidity2.UI.Core;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Windows
{
    public class LoginWindow : Window, IEventHandler<IdentityAuthorizationSuccessEvent>,
        ITemplateLoader, IEventObserver
    {
        private LoginViewModel _viewModel;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWindowCommonBehavior _windowCommonBehavior;
        private bool _showPassword;

        public LoginWindow(IAuthenticationService authenticationService,
            IWindowCommonBehavior windowCommonBehavior)
        {
            _authenticationService = authenticationService;
            _windowCommonBehavior = windowCommonBehavior;
            windowCommonBehavior.SetEffectWindow(this);
            _windowCommonBehavior.BorderColorChanged += OnBorderColorChanged;

            InitViewModel();
            DataContext = _viewModel;

            this.AddCommandBinding(_viewModel.ShowPasswordButtonClickCmd, ShowPasswordButton_Click_CanExecute, ShowPasswordButton_Click_Executed);
            this.AddCommandBinding(_viewModel.LoginButtonClickCmd, LoginButton_Click_CanExecute, LoginButton_Click_Executed);
            this.AddCommandBinding(_viewModel.WindowCloseCmd, WindowClose_Click_CanExecute, WindowClose_Click_Executed);


            _viewModel.AccountText = "15736262776";
            _viewModel.PasswordText = "jin15736262776";
        }

        private void InitViewModel()
        {
            _viewModel = new LoginViewModel
            {
                WindowDragMoveCmd = _windowCommonBehavior.WindowDragMoveCmd,
                WindowMinimizeCmd = _windowCommonBehavior.WindowMinimizeCmd,
                WindowCloseCmd = new CustomRoutedCommand("LoginWindowCloseCmd", typeof(LoginWindow)),
                ShowPasswordButtonClickCmd = new CustomRoutedCommand("ShowPasswordButtonClickCmd", typeof(LoginWindow)),
                LoginButtonClickCmd = new CustomRoutedCommand("LoginButtonClickCmd", typeof(LoginWindow)),
                //应做在XAML上绑定，而非在程序赋值
                ShowPasswordButtonBrush = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99))
            };
        }
        private void OnBorderColorChanged(object sender, PropertyChangedEventArgs e)
        {
            _viewModel.BorderColor = _windowCommonBehavior.BorderColor;
        }

        private void ShowPasswordButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ShowPasswordButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _showPassword = !_showPassword;
            //最佳做法应使用Trigger在XAML触发，而非在程序控制
            if (_showPassword)
            {
                _viewModel.TextBoxVisibility = Visibility.Visible;
                _viewModel.PasswordBoxVisibility = Visibility.Hidden;
                _viewModel.ShowPasswordButtonBrush = new SolidColorBrush(Color.FromRgb(0x15, 0x64, 0xF7));
            }
            else
            {
                _viewModel.TextBoxVisibility = Visibility.Hidden;
                _viewModel.PasswordBoxVisibility = Visibility.Visible;
                _viewModel.ShowPasswordButtonBrush = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
            }
        }

        private void LoginButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void WindowClose_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void WindowClose_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
            e.Handled = true;
        }

        private void LoginButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.AccountText))
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.PasswordText))
                {
                    if (_viewModel.ReadTermsOfUse)
                    {
                        try
                        {
                            _authenticationService.Authorization(_viewModel.AccountText, _viewModel.PasswordText);
                        }
                        catch (Exception ex)
                        {
                            _viewModel.ErrorMessage = ex.Message;
                        }
                    }
                    else
                    {
                        _viewModel.ErrorMessage = "请阅读并同意软件《使用条款》";
                    }
                }
                else
                {
                    _viewModel.ErrorMessage = "请输入密码";
                }
            }
            else
            {
                _viewModel.ErrorMessage = "请输入账号";
            }
        }

        public Task Handle(IdentityAuthorizationSuccessEvent @event, CancellationToken token)
        {
            Close();
            return Task.CompletedTask;
        }

        public void LoadeTemplate() => _windowCommonBehavior.LoadeTemplate();

        public void Subscribe(IEventBusRegistrator registrator)
        {
            registrator.Register(this);
        }
    }
}
