using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public class StarButtonBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelfSelect)
            {
                Color color = (Color)ColorConverter.ConvertFromString(isSelfSelect == true ? "#FFFFFF" : "#4D5160"
                    );
                Brush brush = new SolidColorBrush(color);
                return brush;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
