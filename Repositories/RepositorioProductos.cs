using System.Collections.Generic;
using System.Linq;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;

namespace MauiAppGestorMovil.Repositories
{
    /// <summary>
    /// Repositorio encargado de la persistencia y gestión en memoria de productos.
    /// Lee y guarda datos desde/hacia el archivo productos.json.
    /// </summary>
    public class RepositorioProductos
    {
        private const string RutaArchivo = "productos.json";
        private List<Producto> productos;

        /// <summary>
        /// Constructor: carga la lista de productos desde el almacenamiento local.
        /// </summary>
        public RepositorioProductos()
        {
            productos = Persistencia.Cargar<Producto>(RutaArchivo);
        }

        /// <summary>
        /// Devuelve todos los productos cargados actualmente en memoria.
        /// </summary>
        public List<Producto> ObtenerTodos()
        {
            return productos;
        }

        /// <summary>
        /// Guarda en el archivo productos.json el estado actual de la lista.
        /// </summary>
        public void GuardarTodos()
        {
            Persistencia.Guardar(RutaArchivo, productos);
        }

        /// <summary>
        /// Agrega un nuevo producto a la lista y lo guarda en disco.
        /// </summary>
        public void Agregar(Producto nuevo)
        {
            productos.Add(nuevo);
            GuardarTodos();
        }

        /// <summary>
        /// Elimina un producto por su Id y guarda los cambios.
        /// </summary>
        public void Eliminar(int id)
        {
            productos.RemoveAll(p => p.Id == id);
            GuardarTodos();
        }

        /// <summary>
        /// Actualiza los datos de un producto existente y guarda los cambios.
        /// </summary>
        public void Actualizar(Producto actualizado)
        {
            int index = productos.FindIndex(p => p.Id == actualizado.Id);
            if (index != -1)
            {
                productos[index] = actualizado;
                GuardarTodos();
            }
        }

        /// <summary>
        /// Busca un producto por su identificador único.
        /// </summary>
        public Producto? BuscarPorId(int id)
        {
            return productos.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Devuelve todos los productos que pertenecen a una categoría específica.
        /// </summary>
        /// <param name="categoriaId">Id de la categoría a buscar.</param>
        public List<Producto> BuscarPorCategoriaId(int categoriaId)
        {
            return productos.Where(p => p.IdCategoria == categoriaId).ToList();
        }

        /// <summary>
        /// Verifica si existe un producto con el nombre dado (ignora mayúsculas/minúsculas).
        /// </summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <returns>true si existe, false si no.</returns>
        public bool ExistePorNombre(string nombre)
        {
            return productos.Any(p => string.Equals(p.Nombre, nombre, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}