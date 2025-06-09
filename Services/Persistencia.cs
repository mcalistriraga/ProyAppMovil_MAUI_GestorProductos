using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MauiAppGestorMovil.Services
{
    public static class Persistencia
    {
        private static string CarpetaDatos =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static void Guardar<T>(string nombreArchivo, List<T> datos)
        {
            string ruta = Path.Combine(CarpetaDatos, nombreArchivo);
            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ruta, json);
        }

        public static List<T> Cargar<T>(string nombreArchivo)
        {
            string ruta = Path.Combine(CarpetaDatos, nombreArchivo);
            if (!File.Exists(ruta))
                return new List<T>();

            var json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
