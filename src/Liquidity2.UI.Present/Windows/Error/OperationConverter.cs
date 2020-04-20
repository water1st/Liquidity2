using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Liquidity2.UI.Present.Windows.Error
{
    class OperationConverter : IValueConverter
    {
        private readonly IDictionary<string, string> operationType = new Dictionary<string, string> {
            {"0","买单" },
            {"1","卖单" },
            {"2","撤单"}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                var result = operationType[strValue];
                return result;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
