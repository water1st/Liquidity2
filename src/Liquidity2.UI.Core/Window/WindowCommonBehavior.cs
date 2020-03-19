using Liquidity2.Extensions.WindowPostions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

namespace Liquidity2.UI.Core
{
    public class WindowCommonBehavior : IWindowCommonBehavior
    {
        private Window _window;

        public void SetEffectWindow(Window window)
        {

            _window = window;
            var type = _window.GetType();

            WindowDragMoveCmd = new CustomRoutedCommand(nameof(WindowDragMoveCmd), type, _window);
            _window.AddCommandBinding(WindowDragMoveCmd, Window_DragMove_CanExecute, Window_DragMove_Executed);

            WindowMinimizeCmd = new CustomRoutedCommand(nameof(WindowMinimizeCmd), type, _window);
            _window.AddCommandBinding(WindowMinimizeCmd, Window_Minimize_CanExecute, Window_Minimize_Executed);

            WindowMaximizeCmd = new CustomRoutedCommand(nameof(WindowMaximizeCmd), type, _window);
            _window.AddCommandBinding(WindowMaximizeCmd, Window_Maximize_CanExecute, Window_Maximize_Executed);

            WindowCloseCmd = new CustomRoutedCommand(nameof(WindowCloseCmd), type, _window);
            _window.AddCommandBinding(WindowCloseCmd, Window_Close_CanExecute, Window_Close_Executed);

            WindowSearchCmd = new CustomRoutedCommand(nameof(WindowSearchCmd), type, _window);
            _window.AddCommandBinding(WindowSearchCmd, Window_Search_CanExecute, Window_Search_Executed);

            WindowGroupCmd = new CustomRoutedCommand(nameof(WindowGroupCmd), type, _window);
            _window.AddCommandBinding(WindowGroupCmd, Window_Group_CanExecute, Window_Group_Executed);

            _window.Activated += Window_Activated;
            _window.Deactivated += Window_Deactivated;
        }

        private bool _highlightBorder = true;

        public bool HighlightBorder
        {
            get => _highlightBorder;
            set
            {
                if (value)
                {
                    _borderColor = Color.FromArgb(0x0, 0xFF, 0xFF, 0xFF);
                }
                _highlightBorder = value;
            }
        }

        private Color _settedBorderColor = Color.FromRgb(0x22, 0x75, 0xFF);
        private Color _borderColor = Color.FromRgb(0x22, 0x75, 0xFF);
        public Color BorderColor
        {
            get => _borderColor;
            set => _borderColor = _settedBorderColor = value;
        }

        public event PropertyChangedEventHandler BorderColorChanged;

        public bool CanDragMove { get; set; } = true;

        public ICommand WindowDragMoveCmd { get; private set; }

        public bool CanMinimize { get; set; } = true;

        public ICommand WindowMinimizeCmd { get; private set; }

        public bool CanMaximize { get; set; } = true;

        public ICommand WindowMaximizeCmd { get; private set; }

        public bool CanClose { get; set; } = true;

        public ICommand WindowCloseCmd { get; private set; }

        public bool CanSearch { get; set; } = true;

        public ICommand WindowSearchCmd { get; private set; }

        public event PropertyChangedEventHandler WindowSearchChanged;

        public bool CanGroup { get; set; } = true;

        public ICommand WindowGroupCmd { get; private set; }

        private WindowPostion _postion;
        public WindowPostion Postion
        {
            get => _postion;
            set
            {
                if (value != null)
                {
                    _postion = value;
                    _window.Height = _postion.Height;
                    _window.Width = _postion.Width;
                    _window.Left = _postion.Left;
                    _window.Top = _postion.Top;
                }
            }
        }
        public event PropertyChangedEventHandler WindowGroupChanged;

        private void Window_DragMove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement uIElement && uIElement.IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void Window_DragMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanDragMove)
            {
                _window.DragMove();
            }
        }

        private void Window_Minimize_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement uIElement && uIElement.IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void Window_Minimize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanMinimize)
            {
                _window.WindowState = WindowState.Minimized;
            }
        }

        private void Window_Maximize_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement uIElement && uIElement.IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void Window_Maximize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanMaximize)
            {
                _window.WindowState = _window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }

        private void Window_Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement uIElement && uIElement.IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
            e.Handled = true;
        }

        private void Window_Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanClose)
            {
                _window.Close();
            }
        }

        private void Window_Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Window_Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanSearch && e.Parameter != null)
            {
                WindowSearchChanged?.Invoke(sender, new PropertyChangedEventArgs(e.Parameter.ToString()));
            }
        }

        private void Window_Group_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Window_Group_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanGroup)
            {
                WindowGroupChanged?.Invoke(sender, new PropertyChangedEventArgs(e.Parameter.ToString()));
            }
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            if (HighlightBorder)
            {
                _borderColor = Color.FromArgb(0x0, 0xFF, 0xFF, 0xFF);
                BorderColorChanged?.Invoke(sender, new PropertyChangedEventArgs(nameof(Window_Deactivated)));
            }
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (HighlightBorder)
            {
                _borderColor = _settedBorderColor;
                BorderColorChanged?.Invoke(sender, new PropertyChangedEventArgs(nameof(Window_Activated)));
            }
        }

        public void LoadeTemplate()
        {
            _window.AllowsTransparency = true;
            _window.WindowStyle = WindowStyle.None;
            string resourceName = $"{GetType().Name}_Style";
            _window.Style = (Style)Application.Current.Resources[resourceName];

            ConfigureWindowChrome();
        }

        private void ConfigureWindowChrome()
        {
            var windowChrome = new WindowChrome
            {
                CaptionHeight = 0,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
                ResizeBorderThickness = new Thickness(3),
                NonClientFrameEdges = NonClientFrameEdges.None
            };
            WindowChrome.SetWindowChrome(_window, windowChrome);
        }
    }
}
