using System.Collections.Generic;
using System.Linq;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;

namespace MauiAppGestorMovil.Repositories
{
    public class RepositorioProductos
    {
        private const string RutaArchivo = "productos.json";
        private List<Producto> productos;

        public RepositorioProductos()
        {
            productos = Persistencia.Cargar<Producto>(RutaArchivo);
        }

        public List<Producto> ObtenerTodos()
        {
            return productos;
        }

        public void GuardarTodos()
        {
            Persistencia.Guardar(RutaArchivo, productos);
        }

        public void Agregar(Producto nuevo)
        {
            productos.Add(nuevo);
            GuardarTodos();
        }

        public void Eliminar(int id)
        {
            productos.RemoveAll(p => p.Id == id);
            GuardarTodos();
        }

        public void Actualizar(Producto actualizado)
        {
            int index = productos.FindIndex(p => p.Id == actualizado.Id);
            if (index != -1)
            {
                productos[index] = actualizado;
                GuardarTodos();
            }
        }

        public Producto? BuscarPorId(int id)
        {
            return productos.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> BuscarPorCategoriaId(int categoriaId)
        {
            return productos.Where(p => p.IdCategoria == categoriaId).ToList();
        }
    }
}
