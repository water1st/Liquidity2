using Liquidity2.Extensions.WindowPostions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Core
{
    public class WindowBase : Window, INotifyPropertyChanged, IWindowPostion, ITemplateLoader
    {
        private readonly IWindowCommonBehavior _windowCommonBehavior;
        protected bool GroupActivate;

        private bool _hasGroup;
        public bool HasGroup
        {
            get => _hasGroup;
            set => _windowCommonBehavior.CanGroup = _hasGroup = value;
        }

        private bool _hasSearch;
        public bool HasSearch
        {
            get => _hasSearch;
            set => _windowCommonBehavior.CanSearch = _hasSearch = value;
        }

        protected Guid WindowId { get; }
        public ICommand WindowDragMoveCmd => _windowCommonBehavior.WindowDragMoveCmd;
        public ICommand WindowMinimizeCmd => _windowCommonBehavior.WindowMinimizeCmd;
        public ICommand WindowMaximizeCmd => _windowCommonBehavior.WindowMaximizeCmd;
        public ICommand WindowCloseCmd => _windowCommonBehavior.WindowCloseCmd;
        public ICommand WindowSearchedCmd => _windowCommonBehavior.WindowSearchCmd;
        public ICommand WindowGroupedCmd => _windowCommonBehavior.WindowGroupCmd;

        private Visibility groupBtnVisibility;
        public Visibility GroupBtnVisibility
        {
            get => groupBtnVisibility;
            set
            {
                groupBtnVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupBtnVisibility)));
            }
        }

        private Visibility searchedBtnVisibility;
        public Visibility SearchedBtnVisibility
        {
            get => searchedBtnVisibility;
            set
            {
                searchedBtnVisibility = value;
                OnPropertyChanged(nameof(SearchedBtnVisibility));
            }
        }

        private Brush windowBorderBrush;

        public Brush WindowBorderBrush
        {
            get => windowBorderBrush;
            set
            {
                windowBorderBrush = value;
                OnPropertyChanged(nameof(WindowBorderBrush));
            }
        }

        public WindowPostion Postion { get => _windowCommonBehavior.Postion; set => _windowCommonBehavior.Postion = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WindowBase(IWindowCommonBehavior windowCommonBehavior)
        {
            _windowCommonBehavior = windowCommonBehavior;
            _windowCommonBehavior.SetEffectWindow(this);
            Loaded += InitializeTitle;
            WindowId = Guid.NewGuid();
            DataContext = this;

            //成为前台窗口
            Activated += WindowBase_Activated;
            //成为后台窗口
            Deactivated += WindowBase_Deactivated;
        }

        private void WindowBase_Activated(object sender, EventArgs e)
        {
            WindowBorderBrush = new SolidColorBrush(Color.FromRgb(0x22, 0x75, 0xFF));
        }

        private void WindowBase_Deactivated(object sender, EventArgs e)
        {
            WindowBorderBrush = Brushes.Transparent;
        }

        protected virtual void OnGroupChanged(string group) { }
        protected virtual void OnSearchText(string searchText) { }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void InitializeTitle(object initializeSender, RoutedEventArgs initializeE)
        {
            if (HasGroup)
            {
                _windowCommonBehavior.WindowGroupChanged += WindowGroupChanged;
                GroupBtnVisibility = Visibility.Visible;
            }
            else
            {
                GroupBtnVisibility = Visibility.Collapsed;
            }
            if (HasSearch)
            {
                _windowCommonBehavior.WindowSearchChanged += WindowSearchChanged;
                SearchedBtnVisibility = Visibility.Visible;
            }
            else
            {
                SearchedBtnVisibility = Visibility.Collapsed;
            }
        }

        private void WindowGroupChanged(object sender, PropertyChangedEventArgs e)
        {
            //改变groupButton
            if (HasGroup)
            {
                GroupActivate = e.PropertyName != "/";
                OnGroupChanged(e.PropertyName);
            }
        }

        private void WindowSearchChanged(object sender, PropertyChangedEventArgs e)
        {
            OnSearchText(e.PropertyName);
        }

        public void LoadeTemplate() => _windowCommonBehavior.LoadeTemplate();
    }
}
