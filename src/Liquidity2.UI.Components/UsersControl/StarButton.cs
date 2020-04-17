using System.Windows;
using System.Windows.Controls;

namespace Liquidity2.UI.Components.UsersControl
{
    public class StarButton : Button
    {
        public string StarStringContent
        {
            get { return (string)GetValue(StarContent); }

            set { SetValue(StarContent, value); }
        }

        public static readonly DependencyProperty StarContent = DependencyProperty.Register(nameof(StarStringContent), typeof(string), typeof(StarButton));

        public bool StarIsSelfSelect
        {
            get { return (bool)GetValue(IsSelfSelect); }

            set { SetValue(IsSelfSelect, value); }
        }

        public static readonly DependencyProperty IsSelfSelect = DependencyProperty.Register(nameof(StarIsSelfSelect), typeof(bool), typeof(StarButton));
    }
}
