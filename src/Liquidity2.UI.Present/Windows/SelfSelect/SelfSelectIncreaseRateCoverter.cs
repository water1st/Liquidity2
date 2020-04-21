using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Liquidity2.UI.Windows.SelfSelect
{
    public class SelfSelectIncreaseRateCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double dvalue)
            {
                if (dvalue >= 0)
                {
                    return "+" + dvalue.ToString("P");
                }
                else
                {
                    return dvalue.ToString("P");
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

    }
}
