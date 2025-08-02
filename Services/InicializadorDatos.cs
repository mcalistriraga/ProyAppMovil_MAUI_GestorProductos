    using System.Reflection;
    using System.Text;
    using System.IO;

    namespace MauiAppGestorMovil.Services
    {
        /// <summary>
        /// Clase estática responsable de inicializar los archivos de datos JSON
        /// copiando los archivos de recursos embebidos a la carpeta de datos locales,
        /// solo si dichos archivos no existen aún en el dispositivo.
        /// </summary>
        public static class InicializadorDatos
        {
            /// <summary>
            /// Método público para inicializar los archivos de datos
            /// </summary>
            public static void InicializarArchivos()
            {
                CopiarArchivoSiNoExiste("productos.json");
                CopiarArchivoSiNoExiste("categorias.json");
            }

            /// <summary>
            /// Copia un archivo desde los recursos embebidos al almacenamiento local
            /// si el archivo no existe aún.
            /// </summary>
            /// <param name="nombreArchivo">Nombre del archivo a copiar</param>
            private static void CopiarArchivoSiNoExiste(string nombreArchivo)
            {
                // Obtiene la ruta completa donde se almacenarán los archivos en el dispositivo
                string rutaDestino = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

                // Verifica si el archivo ya existe para evitar sobrescribirlo
                if (!File.Exists(rutaDestino))
                {
                    // Obtiene el ensamblado actual para acceder a los recursos embebidos
                    var assembly = Assembly.GetExecutingAssembly();

                    // Busca el nombre completo del recurso que termina con el nombre del archivo
                    string? recurso = assembly
                        .GetManifestResourceNames()
                        .FirstOrDefault(r => r.EndsWith(nombreArchivo));

                    if (recurso != null)
                    {
                        // Abre el flujo de lectura del recurso embebido
                        using Stream? stream = assembly.GetManifestResourceStream(recurso);
                        using var reader = new StreamReader(stream!, Encoding.UTF8);

                        // Lee todo el contenido del archivo embebido
                        string contenido = reader.ReadToEnd();

                        // Escribe el contenido en el archivo destino
                        File.WriteAllText(rutaDestino, contenido);
                    }
                }
            }
        }
    }
