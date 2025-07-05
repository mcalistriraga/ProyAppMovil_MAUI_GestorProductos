using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        // Convierte int a bool: 0 -> false, cualquier otro -> true
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0;
            }
            return false;
        }

        // No necesario en este caso, devuelve Binding.DoNothing
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
