using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Converters
{
    public class IntIncrementConverter : IValueConverter
    {
        public int Incremento { get; set; } = 10; // por defecto

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
                return intValue + Incremento;

            return Incremento;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return 0; // no se usa ConvertBack
        }
    }
}
