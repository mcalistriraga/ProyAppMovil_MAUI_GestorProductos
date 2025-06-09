using System.Collections.Generic;
using System.Linq;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;
using MauiAppGestorMovil.Repositories;

namespace MauiAppGestorMovil.Repositories
{
    public class RepositorioCategorias
    {
        private const string RutaArchivo = "categorias.json";
        private List<Categoria> categorias;

        public RepositorioCategorias()
        {
            categorias = Persistencia.Cargar<Categoria>(RutaArchivo);
        }

        public List<Categoria> ObtenerTodas()
        {
            return categorias;
        }

        public void GuardarTodas()
        {
            Persistencia.Guardar(RutaArchivo, categorias);
        }

        public void Agregar(Categoria nueva)
        {
            categorias.Add(nueva);
            GuardarTodas();
        }

        public void Eliminar(int id)
        {
            categorias.RemoveAll(c => c.Id == id);
            GuardarTodas();
        }

        public void Actualizar(Categoria actualizada)
        {
            int index = categorias.FindIndex(c => c.Id == actualizada.Id);
            if (index != -1)
            {
                categorias[index] = actualizada;
                GuardarTodas();
            }
        }

        public Categoria? BuscarPorId(int id)
        {
            return categorias.FirstOrDefault(c => c.Id == id);
        }

        public bool CategoriaEnUso(int idCategoria, RepositorioProductos repoProductos)
        {
            return repoProductos.BuscarPorCategoriaId(idCategoria).Count > 0;
        }
    }
}
