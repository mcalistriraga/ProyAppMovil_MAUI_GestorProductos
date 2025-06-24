using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace MauiAppGestorMovil.Services
{
    /// <summary>
    /// Clase estática que encapsula métodos genéricos para guardar y cargar
    /// listas de objetos en archivos JSON ubicados en el almacenamiento local del dispositivo.
    /// </summary>
    public static class Persistencia
    {
        /// <summary>
        /// Propiedad que obtiene la ruta de la carpeta de datos locales
        /// asignada a la aplicación en el dispositivo.
        /// </summary>
        private static string CarpetaDatos =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Guarda una lista genérica de datos en un archivo JSON.
        /// Si el archivo no existe, lo crea; si existe, lo sobrescribe.
        /// </summary>
        /// <typeparam name="T">Tipo de datos a serializar y guardar</typeparam>
        /// <param name="nombreArchivo">Nombre del archivo JSON</param>
        /// <param name="datos">Lista de datos a guardar</param>
        public static void Guardar<T>(string nombreArchivo, List<T> datos)
        {
            string ruta = Path.Combine(CarpetaDatos, nombreArchivo);

            // Serializa la lista de objetos a JSON con formato indentado para legibilidad
            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });

            // Escribe el JSON en el archivo especificado
            File.WriteAllText(ruta, json);
        }

        /// <summary>
        /// Carga una lista genérica de datos desde un archivo JSON.
        /// Si el archivo no existe, devuelve una lista vacía.
        /// </summary>
        /// <typeparam name="T">Tipo de datos a deserializar</typeparam>
        /// <param name="nombreArchivo">Nombre del archivo JSON</param>
        /// <returns>Lista de datos cargados desde el archivo</returns>
        public static List<T> Cargar<T>(string nombreArchivo)
        {
            string ruta = Path.Combine(CarpetaDatos, nombreArchivo);

            // Si el archivo no existe, retorna lista vacía para evitar errores
            if (!File.Exists(ruta))
                return new List<T>();

            // Lee el contenido JSON del archivo
            var json = File.ReadAllText(ruta);

            // Deserializa el JSON a la lista del tipo especificado
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
