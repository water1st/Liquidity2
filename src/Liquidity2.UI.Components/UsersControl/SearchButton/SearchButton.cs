using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liquidity2.UI.Components.UsersControl.Buttons
{
    public class SearchButton : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand InputCmd { get; set; }
        public ICommand InitializationCmd { get; set; }

        private Visibility _searchControlVisibility;
        private Visibility _searchBtnGridVisibility;
        public Visibility SearchControlVisibility
        {
            get { return _searchControlVisibility; }
            set
            {
                _searchControlVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchControlVisibility)));
            }
        }
        public Visibility SearchBtnGridVisibility
        {
            get { return _searchBtnGridVisibility; }
            set
            {
                _searchBtnGridVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchBtnGridVisibility)));
            }
        }

        private bool _popupOpen;
        public bool PopupOpen
        {
            get { return _popupOpen; }
            set
            {
                _popupOpen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PopupOpen)));
                ChangeVisibility();
            }
        }

        public SearchButton()
        {
            DataContext = this;

            //初始化search样式
            SearchControlVisibility = Visibility.Collapsed;
            SearchBtnGridVisibility = Visibility.Visible;

            InputCmd = new CustomRoutedCommand(nameof(InputCmd), typeof(SearchButton), this);
            this.AddCommandBinding(InputCmd, InputCmdcb_CanExecute, InputCmdcb_Executed);

            InitializationCmd = new CustomRoutedCommand(nameof(InitializationCmd), typeof(SearchButton), this);
            this.AddCommandBinding(InitializationCmd, Initializationcb_CanExecute, Initializationcb_Executed);
        }

        private void Initializationcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PopupOpen = false;
            SearchBtnGridVisibility = Visibility.Visible;
            e.Handled = true;
        }

        private void Initializationcb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void InputCmdcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Command?.Execute(e.Parameter);
            e.Handled = true;
        }

        private void InputCmdcb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        //拦截回车路由事件
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
            else { base.OnKeyDown(e); }
        }

        private void ChangeVisibility()
        {
            if (PopupOpen == false)
            {
                SearchBtnGridVisibility = Visibility.Visible;
            }
        }

        protected override void OnClick()
        {
            SearchBtnGridVisibility = Visibility.Collapsed;
            PopupOpen = true;
        }
    }
}
