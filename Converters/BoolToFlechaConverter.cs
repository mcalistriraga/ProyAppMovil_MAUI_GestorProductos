using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Converters
{
    public class BoolToFlechaConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            // Usamos símbolos compatibles con la mayoría de fuentes móviles
            return (value is bool b && b) ? "▼" : "▶";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            throw new NotImplementedException();
        }
    }
}
