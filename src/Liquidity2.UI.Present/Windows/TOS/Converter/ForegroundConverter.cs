using Liquidity2.UI.Services.DTO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Liquidity2.UI.Windows.TOS.Converter
{
    public class ForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == TradeDirection.Sell.ToString())
            {
                Color color = (Color)ColorConverter.ConvertFromString("#EB2832");//#EB2832
                Brush brush = new SolidColorBrush(color);
                return brush;
            }
            else if (value.ToString() == TradeDirection.Buy.ToString())
            {
                Color color = (Color)ColorConverter.ConvertFromString("#12BD21");//#12BD21
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
