using System.Collections.Generic;
using System.Linq;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;

namespace MauiAppGestorMovil.Repositories
{
    /// <summary>
    /// Repositorio encargado de gestionar la lista de categorías,
    /// incluyendo carga, guardado y operaciones CRUD sobre el archivo categorias.json.
    /// </summary>
    public class RepositorioCategorias
    {
        private const string RutaArchivo = "categorias.json";
        private List<Categoria> categorias;

        /// <summary>
        /// Constructor que carga las categorías desde almacenamiento local.
        /// </summary>
        public RepositorioCategorias()
        {
            categorias = Persistencia.Cargar<Categoria>(RutaArchivo);
        }

        /// <summary>
        /// Devuelve la lista completa de categorías en memoria.
        /// </summary>
        public List<Categoria> ObtenerTodas()
        {
            return categorias;
        }

        /// <summary>
        /// Guarda la lista actual de categorías en el archivo JSON.
        /// </summary>
        public void GuardarTodas()
        {
            Persistencia.Guardar(RutaArchivo, categorias);
        }

        /// <summary>
        /// Agrega una nueva categoría a la lista y guarda el archivo.
        /// </summary>
        public void Agregar(Categoria nueva)
        {
            categorias.Add(nueva);
            GuardarTodas();
        }

        /// <summary>
        /// Elimina una categoría si no tiene subcategorías.
        /// Devuelve true si se eliminó correctamente, false si tiene subcategorías asociadas.
        /// </summary>
        /// <param name="id">Id de la categoría a eliminar.</param>
        public bool Eliminar(int id)
        {
            if (TieneSubcategorias(id))
            {
                // Previene eliminación de categorías con subcategorías.
                return false;
            }

            categorias.RemoveAll(c => c.Id == id);
            GuardarTodas();
            return true;
        }

        /// <summary>
        /// Verifica si la categoría indicada tiene subcategorías asociadas.
        /// </summary>
        /// <param name="id">Id de la categoría a consultar.</param>
        /// <returns>True si tiene subcategorías, false si no.</returns>
        public bool TieneSubcategorias(int id)
        {
            return categorias.Any(c => c.IdPadre == id);
        }

        /// <summary>
        /// Actualiza una categoría existente en base a su Id.
        /// </summary>
        /// <param name="actualizada">Categoría con nuevos datos.</param>
        public void Actualizar(Categoria actualizada)
        {
            int index = categorias.FindIndex(c => c.Id == actualizada.Id);
            if (index != -1)
            {
                categorias[index] = actualizada;
                GuardarTodas();
            }
        }

        /// <summary>
        /// Busca una categoría por su Id.
        /// </summary>
        public Categoria? BuscarPorId(int id)
        {
            return categorias.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Verifica si una categoría está siendo utilizada por al menos un producto.
        /// Requiere pasar una instancia de RepositorioProductos.
        /// </summary>
        public bool CategoriaEnUso(int idCategoria, RepositorioProductos repoProductos)
        {
            return repoProductos.BuscarPorCategoriaId(idCategoria).Count > 0;
        }

        /// <summary>
        /// Genera un nuevo Id único para una nueva categoría.
        /// </summary>
        public int GenerarNuevoId()
        {
            return categorias.Any() ? categorias.Max(c => c.Id) + 1 : 1;
        }

        /// <summary>
        /// Obtiene todas las categorías descendientes (subcategorías) de una categoría.
        /// </summary>
        public List<Categoria> ObtenerSubcategoriasRecursivas(int idCategoria)
        {
            List<Categoria> resultado = new();
            ObtenerSubcategoriasRecursivasAux(idCategoria, resultado);
            return resultado;
        }

        private void ObtenerSubcategoriasRecursivasAux(int idPadre, List<Categoria> acumulador)
        {
            var hijos = categorias.Where(c => c.IdPadre == idPadre).ToList();
            foreach (var hijo in hijos)
            {
                acumulador.Add(hijo);
                ObtenerSubcategoriasRecursivasAux(hijo.Id, acumulador);
            }
        }

        /// <summary>
        /// Intenta eliminar una categoría y todas sus subcategorías recursivamente.
        /// Solo elimina si ninguna está en uso.
        /// </summary>
        public (bool eliminado, List<string> categoriasEnUso, List<string> categoriasAEliminar)
            EliminarConSubcategorias(int idCategoria, RepositorioProductos repoProductos)
        {
            var todasAEliminar = new List<Categoria> { BuscarPorId(idCategoria)! };
            todasAEliminar.AddRange(ObtenerSubcategoriasRecursivas(idCategoria));

            List<string> enUso = new();
            foreach (var cat in todasAEliminar)
            {
                if (CategoriaEnUso(cat.Id, repoProductos))
                    enUso.Add(cat.Nombre);
            }

            if (enUso.Any())
                return (false, enUso, todasAEliminar.Select(c => c.Nombre).ToList());

            // Eliminar categorías
            foreach (var cat in todasAEliminar)
                categorias.RemoveAll(c => c.Id == cat.Id);

            GuardarTodas();
            return (true, new(), todasAEliminar.Select(c => c.Nombre).ToList());
        }

    }
}
