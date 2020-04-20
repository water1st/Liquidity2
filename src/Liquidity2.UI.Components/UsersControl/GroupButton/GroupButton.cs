using Liquidity2.UI.Components.Windows;
using Liquidity2.UI.Core;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Liquidity2.UI.Components.UsersControl.GroupButton
{
    public class GroupButton : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Window targetWindow;
        private WindowPosition position;
        private GroupWindow window;
        private readonly IWindowFactory windowFactory;

        private bool Hascanvas;
        private bool Hasblock;
        public string InputWindowType { get; set; }

        private Visibility canvasVisibility;
        public Visibility CanvasVisibility
        {
            get => canvasVisibility;
            set
            {
                canvasVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanvasVisibility)));
            }
        }

        private Visibility blockVisibility;
        public Visibility BlockVisibility
        {
            get => blockVisibility;
            set
            {
                blockVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BlockVisibility)));
            }
        }

        private string groupText;
        public string GroupText
        {
            get => groupText;
            set
            {
                groupText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupText)));
            }
        }

        public GroupButton()
        {
            windowFactory = Core.Container.Insecure.GetService<IWindowFactory>();
            DataContext = this;
        }

        public void GroupButton_GroupStyleChanged(string groupText)
        {
            if (groupText != "/")
            {
                Hascanvas = false;
                Hasblock = true;
                GroupText = groupText;
            }
            else
            {
                Hasblock = false;
                Hascanvas = true;
            }
            BlockVisibility = Hasblock ? Visibility.Visible : Visibility.Hidden;
            CanvasVisibility = Hascanvas ? Visibility.Visible : Visibility.Hidden;
        }

        protected override void OnClick()
        {
            var window = CreateWindow();
            window.Show(position);
        }

        private GroupWindow CreateWindow()
        {
            window = windowFactory.Create<GroupWindow>();

            //拓展RouteCommand
            window.InputCmd = new CustomRoutedCommand(nameof(window.InputCmd), typeof(GroupButton), this);
            this.AddCommandBinding(window.InputCmd, InputCmdcb_CanExecute, InputCmdcb_Executed);

            targetWindow = Window.GetWindow(this);
            position = new WindowPosition() { Left = targetWindow.Left, Top = targetWindow.Top, Width = targetWindow.Width, Height = targetWindow.Height };

            return window;
        }

        private void InputCmdcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Command?.Execute(e.Parameter.ToString());
            GroupButton_GroupStyleChanged(e.Parameter.ToString());
            window.CloseWindow();
            e.Handled = true;
        }

        private void InputCmdcb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
