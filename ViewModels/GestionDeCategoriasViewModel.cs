using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MauiAppGestorMovil.ViewModels
{
    public class GestionDeCategoriasViewModel : BaseViewModel
    {
        private readonly RepositorioCategorias _repoCategorias;

        public ObservableCollection<CategoriaNodo> CategoriasJerarquicas { get; set; } = new();

        public GestionDeCategoriasViewModel(RepositorioCategorias repo)
        {
            _repoCategorias = repo;
            ConstruirJerarquiaDeCategorias();
        }

        private void ConstruirJerarquiaDeCategorias()
        {
            var todas = _repoCategorias.ObtenerTodas();
            var mapaNodos = new Dictionary<int, CategoriaNodo>();

            // Paso 1: crear nodos individuales
            foreach (var cat in todas)
            {
                mapaNodos[cat.Id] = new CategoriaNodo(cat);
            }

            // Paso 2: enlazar nodos como árbol
            CategoriasJerarquicas.Clear();

            foreach (var nodo in mapaNodos.Values)
            {
                if (nodo.Categoria.IdPadre == null)
                {
                    // Categoría raíz
                    CategoriasJerarquicas.Add(nodo);
                }
                else if (mapaNodos.TryGetValue(nodo.Categoria.IdPadre.Value, out var padre))
                {
                    padre.Subcategorias.Add(nodo);
                }
            }
        }

        public void Recargar()
        {
            ConstruirJerarquiaDeCategorias();
        }
    }
}
