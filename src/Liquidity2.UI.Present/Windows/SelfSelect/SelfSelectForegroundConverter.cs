using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public class SelfSelectForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double dvalue)
            {
                Color color = (Color)ColorConverter.ConvertFromString(dvalue >= 0 ? "#12BD21" : "#EB2832"
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
