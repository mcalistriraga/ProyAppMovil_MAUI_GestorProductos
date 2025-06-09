using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;
using System.Collections.Generic;
using System.Linq;

namespace MauiAppGestorMovil.Services
{
    public class RepositorioProductos
    {
        private readonly string rutaArchivo;
        private List<Producto> productos;

        public RepositorioProductos(string ruta)
        {
            rutaArchivo = ruta;
            productos = Persistencia.Cargar<Producto>(rutaArchivo);
        }

        public List<Producto> ObtenerTodos()
        {
            return productos;
        }

        public void Agregar(Producto nuevo)
        {
            productos.Add(nuevo);
            Persistencia.Guardar<Producto>(rutaArchivo, productos);
        }

        public void Eliminar(int id)
        {
            productos.RemoveAll(p => p.Id == id);
            Persistencia.Guardar<Producto>(rutaArchivo, productos);
        }

        public void Actualizar(Producto actualizado)
        {
            int index = productos.FindIndex(p => p.Id == actualizado.Id);
            if (index != -1)
            {
                productos[index] = actualizado;
                Persistencia.Guardar<Producto>(rutaArchivo, productos);
            }
        }

        public Producto? BuscarPorId(int id)
        {
            return productos.FirstOrDefault(p => p.Id == id);
        }
    }
}
