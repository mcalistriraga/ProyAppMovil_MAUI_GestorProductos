using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Helpers
{
    public class DecimalParseResult
    {
        public bool Success { get; set; }
        public decimal Value { get; set; }
    }

    public static class DecimalHelper
    {
        public static async Task<DecimalParseResult> TryParseDecimalWithAlert(string input)
        {
            var culture = CultureInfo.CurrentCulture;
            var decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;

            if (decimal.TryParse(input, NumberStyles.Number, culture, out decimal result))
            {
                return new DecimalParseResult
                {
                    Success = true,
                    Value = result
                };
            }

            var mainPage = Application.Current?.MainPage;
            if (mainPage != null)
            {
                await mainPage.DisplayAlert(
                    "Formato inválido",
                    $"Por favor ingrese un número válido usando '{decimalSeparator}' como separador decimal.",
                    "OK"
                );
            }

            return new DecimalParseResult
            {
                Success = false,
                Value = 0
            };
        }
    }
}
