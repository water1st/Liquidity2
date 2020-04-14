using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Liquidity2.UI.Windows.TOS.Converter
{
    public class BackgroundConverter : IValueConverter
    {

        private readonly string[] _backgrounds = { "#CEA232", "#1F2660", "#9B2D7A", "#0D3041", "#8316D6", "#41160D", "#1282CD", "#314504", "#BC6F29", "#39297E", "#313851", "#171B2B" };

        private Color backgroundColor;
        private Brush backgroundBrush;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                if (index <= 10)
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString(_backgrounds[index]);
                }
                else if (index % 2 == 0)
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString(_backgrounds[10]);
                }
                else
                {
                    backgroundColor = (Color)ColorConverter.ConvertFromString(_backgrounds[11]);
                }
                backgroundBrush = new SolidColorBrush(backgroundColor);
                return backgroundBrush;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
