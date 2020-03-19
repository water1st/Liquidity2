using Liquidity2.Extensions.Identity;
using Liquidity2.UI.Core;
using Liquidity2.UI.Services;
using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Windows
{
    public class NavigationWindow : Window, IWindowPostion, ITemplateLoader
    {
        private readonly IWindowCommonBehavior _windowCommonBehavior;
        private readonly IWindowPresentService _presentService;

        public NavigationViewModel NavigationViewModel { get; private set; }
        public WindowPostion Postion { get => _windowCommonBehavior.Postion; set => _windowCommonBehavior.Postion = value; }

        public NavigationWindow(IWindowCommonBehavior windowCommonBehavior, IWindowPresentService presentService)
        {
            _windowCommonBehavior = windowCommonBehavior;
            _presentService = presentService;
            _windowCommonBehavior.SetEffectWindow(this);

            InitViewModel();
            BindingCommand();
        }

        private void InitViewModel()
        {
            NavigationViewModel = new NavigationViewModel
            {
                Account = User.Current.Name,
                AssetButtonClickCmd = new CustomRoutedCommand("AssetButtonClickCmd", typeof(NavigationWindow)),
                TosButtonClickCmd = new CustomRoutedCommand("TosButtonClickCmd", typeof(NavigationWindow)),
                KLineButtonClickCmd = new CustomRoutedCommand("KLineButtonClickCmd", typeof(NavigationWindow)),
                OrderButtonClickCmd = new CustomRoutedCommand("OrderButtonClickCmd", typeof(NavigationWindow)),
                EntrustButtonClickCmd = new CustomRoutedCommand("EntrustButtonClickCmd", typeof(NavigationWindow)),
                SelfSelectButtonClickCmd = new CustomRoutedCommand("SelfSelectButtonClickCmd", typeof(NavigationWindow)),
                AccountButtonClickCmd = new CustomRoutedCommand("AccountButtonClickCmd", typeof(NavigationWindow)),
                ExitButtonClickCmd = new CustomRoutedCommand("ExitButtonClickCmd", typeof(NavigationWindow)),
                InstallButtonClickCmd = new CustomRoutedCommand("InstallButtonClickCmd", typeof(NavigationWindow)),
                ErrorButtonClickCmd = new CustomRoutedCommand("ErrorButtonClickCmd", typeof(NavigationWindow)),
                WindowDragMoveCmd = _windowCommonBehavior.WindowDragMoveCmd,
            };
        }

        private void BindingCommand()
        {
            this.AddCommandBinding(NavigationViewModel.AssetButtonClickCmd, AssetButton_Click_CanExecute, AssetButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.TosButtonClickCmd, TosButton_Click_CanExecute, TosButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.KLineButtonClickCmd, KLineButton_Click_CanExecute, KLineButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.OrderButtonClickCmd, OrderButton_Click_CanExecute, OrderButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.ExitButtonClickCmd, ExitButton_Click_CanExecute, ExitButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.InstallButtonClickCmd, InstallButton_Click_CanExecute, InstallButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.EntrustButtonClickCmd, EntrustButton_Click_CanExecute, EntrustButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.AccountButtonClickCmd, AccountButton_Click_CanExecute, AccountButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.SelfSelectButtonClickCmd, SelfSelectButton_Click_CanExecute, SelfSelectButton_Click_Executed);
            this.AddCommandBinding(NavigationViewModel.ErrorButtonClickCmd, ErrorButton_Click_CanExecute, ErrorButton_Click_Execute);
        }

        private void ErrorButton_Click_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowErrorWindow();
            e.Handled = true;
        }

        private void ErrorButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SelfSelectButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowSelfSelectWindow();
            e.Handled = true;
        }

        private void SelfSelectButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void AccountButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowAccountWindow();
            e.Handled = true;
        }

        private void AccountButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void AssetButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void AssetButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowAssetWindow();
            e.Handled = true;
        }

        private void TosButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void TosButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowTosWindow();
            e.Handled = true;
        }

        private void KLineButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void KLineButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowKLineWindow();
            e.Handled = true;
        }

        private void OrderButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OrderButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowOrderWindow();
            e.Handled = true;
        }

        private void ExitButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void ExitButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
            e.Handled = true;
        }

        private void InstallButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowSettingWindow();
            e.Handled = true;
        }

        private void InstallButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void EntrustButton_Click_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _presentService.ShowEntrustWindow();
            e.Handled = true;
        }

        private void EntrustButton_Click_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        public void LoadeTemplate() => _windowCommonBehavior.LoadeTemplate();
    }
}
