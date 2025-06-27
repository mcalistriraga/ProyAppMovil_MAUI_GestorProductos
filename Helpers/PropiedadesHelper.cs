using System.Collections.Generic;
using System.Linq;

namespace MauiAppGestorMovil.Helpers
{
    public static class PropiedadesHelper
    {
        /// <summary>
        /// Devuelve una cadena formateada con las propiedades del producto.
        /// Ejemplo: "Marca: Dell, Color: Negro"
        /// </summary>
        public static string FormatearPropiedades(Dictionary<string, string> propiedades)
        {
            if (propiedades == null || propiedades.Count == 0)
                return "Sin propiedades";

            return string.Join(", ", propiedades.Select(p => $"{p.Key}: {p.Value}"));
        }
    }
}
