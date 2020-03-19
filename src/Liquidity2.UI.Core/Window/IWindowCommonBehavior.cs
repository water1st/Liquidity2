using Liquidity2.Extensions.WindowPostions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Core
{
    public interface IWindowCommonBehavior : IWindowPostion, ITemplateLoader
    {
        void SetEffectWindow(Window window);

        bool HighlightBorder { get; set; }

        Color BorderColor { get; set; }

        event PropertyChangedEventHandler BorderColorChanged;

        bool CanDragMove { get; set; }

        ICommand WindowDragMoveCmd { get; }

        bool CanMinimize { get; set; }

        ICommand WindowMinimizeCmd { get; }

        bool CanMaximize { get; set; }

        ICommand WindowMaximizeCmd { get; }

        bool CanClose { get; set; }

        ICommand WindowCloseCmd { get; }

        bool CanSearch { get; set; }

        ICommand WindowSearchCmd { get; }

        event PropertyChangedEventHandler WindowSearchChanged;

        bool CanGroup { get; set; }

        ICommand WindowGroupCmd { get; }

        event PropertyChangedEventHandler WindowGroupChanged;
    }
}
