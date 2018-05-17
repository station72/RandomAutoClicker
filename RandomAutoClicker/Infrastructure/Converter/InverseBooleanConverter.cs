using System;
using System.Globalization;
using System.Windows.Data;

namespace RandomAutoClicker.Infrastructure.Converter
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverse(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Inverse(value, targetType);
        }

        private bool Inverse(object value, Type targetType)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }
    }
}
