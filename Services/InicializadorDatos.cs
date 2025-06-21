using System.Reflection;
using System.Text;
using System.IO;

namespace MauiAppGestorMovil.Services
{
    public static class InicializadorDatos
    {
        public static void InicializarArchivos()
        {
            CopiarArchivoSiNoExiste("productos.json");
            CopiarArchivoSiNoExiste("categorias.json");
        }

        private static void CopiarArchivoSiNoExiste(string nombreArchivo)
        {
            string rutaDestino = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

            if (!File.Exists(rutaDestino))
            {
                var assembly = Assembly.GetExecutingAssembly();
                string? recurso = assembly
                    .GetManifestResourceNames()
                    .FirstOrDefault(r => r.EndsWith(nombreArchivo));

                if (recurso != null)
                {
                    using Stream? stream = assembly.GetManifestResourceStream(recurso);
                    using var reader = new StreamReader(stream!, Encoding.UTF8);
                    string contenido = reader.ReadToEnd();

                    File.WriteAllText(rutaDestino, contenido);
                }
            }
        }
    }
}
