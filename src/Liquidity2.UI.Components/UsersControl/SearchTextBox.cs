using Liquidity2.UI.Components.CustomCommand;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liquidity2.UI.Components.UsersControl
{
    public class SearchTextBox : UserControl, IInputCommand, IInitializationCommand
    {
        private readonly SearchTextboxViewModel viewModel;
        private ControlTemplate searchTextCrlTemplate;

        public RoutedCommand SearchEnterCmd { get; } = new RoutedCommand(nameof(SearchEnterCmd), typeof(SearchTextBox));
        public ICommand InputCmd
        {
            get { return (ICommand)GetValue(InputProperty); }

            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty = DependencyProperty.Register(nameof(InputCmd), typeof(ICommand), typeof(SearchTextBox));

        public ICommand InitializationCmd
        {
            get { return (ICommand)GetValue(InitializationProperty); }

            set { SetValue(InitializationProperty, value); }
        }

        public static readonly DependencyProperty InitializationProperty = DependencyProperty.Register(nameof(InitializationCmd), typeof(ICommand), typeof(SearchTextBox));

        public SearchTextBox()
        {
            InitializeTemplates();

            DataContext = viewModel = new SearchTextboxViewModel();

            //得到键盘焦点事件
            GotKeyboardFocus += SearchTextBox_GotKeyboardFocus;
            //失去键盘焦点事件
            LostKeyboardFocus += SearchTextBox_LostKeyboardFocus;
            //定义enter命令
            Loaded += CustomEnterCommand;
        }

        public void Enter()
        {
            InputCmd?.Execute(viewModel.SearchText);
            ReturnInitial();
        }

        private void CustomEnterCommand(object sender, RoutedEventArgs e)
        {
            this.AddCommandBinding(SearchEnterCmd, SearchEnterCb_CanExecute, SearchEnterCb_Executed);
            SearchEnterCmd.InputGestures.Add(new KeyGesture(Key.Enter));
        }

        private void SearchEnterCb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Enter();
            e.Handled = true;
        }

        private void SearchEnterCb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (viewModel.LittleSearchTextGridVis == Visibility.Collapsed)
            {
                e.CanExecute = true;
            }
            else { e.CanExecute = false; }

            //避免继续向上传而降低程序性能
            e.Handled = true;
        }

        private void InitializeTemplates()
        {
            searchTextCrlTemplate = (ControlTemplate)Application.Current.Resources["SearchTextBox_Template"];
            Template = searchTextCrlTemplate;
        }

        //拦截鼠标移动路由事件
        protected override void OnMouseMove(MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void SearchTextBox_LostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            //状态还原
            ReturnInitial();
        }

        //状态还原
        private void ReturnInitial()
        {
            if (viewModel.LittleSearchTextGridVis == Visibility.Collapsed)
            {
                this.ForceCursor = false;
                viewModel.SearchText = null;
                viewModel.LittleSearchTextGridVis = Visibility.Visible;
                InitializationCmd?.Execute(null);
            }
        }

        //得到键盘焦点
        private void SearchTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            viewModel.LittleSearchTextGridVis = Visibility.Collapsed;
        }
    }
}
