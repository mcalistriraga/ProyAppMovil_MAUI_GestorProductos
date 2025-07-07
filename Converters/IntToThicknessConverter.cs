using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Converters
{
    public class IntToThicknessConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
                return new Thickness(intValue, 0, 0, 0);

            return new Thickness(0); // valor por defecto si es nulo o inválido
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return 0; // no se necesita revertir en este caso
        }
    }
}
