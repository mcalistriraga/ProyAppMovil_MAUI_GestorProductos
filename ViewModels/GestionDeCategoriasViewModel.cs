using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels.Helpers;
using MauiAppGestorMovil.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppGestorMovil.ViewModels
{
    public class GestionDeCategoriasViewModel : BaseViewModel
    {
        private readonly RepositorioCategorias _repoCategorias;

        public ObservableCollection<CategoriaNodo> CategoriasJerarquicas { get; } = new();

        /*──────────────  COMANDOS  ──────────────*/
        public ICommand AgregarSubcategoriaCommand { get; }
        public ICommand EditarCategoriaCommand { get; }
        public ICommand EliminarCategoriaCommand { get; }

        public GestionDeCategoriasViewModel(RepositorioCategorias repo)
        {
            _repoCategorias = repo;
            ConstruirJerarquiaDeCategorias();

            AgregarSubcategoriaCommand = new Command<CategoriaNodo>(AgregarSubcategoriaAsync);
            EditarCategoriaCommand = new Command<CategoriaNodo>(EditarCategoriaAsync);
            EliminarCategoriaCommand = new Command<CategoriaNodo>(EliminarCategoriaAsync);
        }

        /*──────── Árbol de categorías ────────*/
        private void ConstruirJerarquiaDeCategorias()
        {
            var todas = _repoCategorias.ObtenerTodas();
            var mapa = new Dictionary<int, CategoriaNodo>();

            foreach (var cat in todas)
                mapa[cat.Id] = new CategoriaNodo(cat);

            CategoriasJerarquicas.Clear();

            foreach (var nodo in mapa.Values)
            {
                if (nodo.Categoria.IdPadre is null)
                    CategoriasJerarquicas.Add(nodo);
                else if (mapa.TryGetValue(nodo.Categoria.IdPadre.Value, out var padre))
                    padre.Subcategorias.Add(nodo);
            }
        }

        public void Recargar() => ConstruirJerarquiaDeCategorias();

        /*──────────➕ Subcategoría──────────*/
        private async void AgregarSubcategoriaAsync(CategoriaNodo? padre)
        {
            if (padre is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;                    //  🡺  protección nulabilidad

            await main.Navigation.PushModalAsync(
                new AgregarSubcategoria(_repoCategorias, padre));
        }

        /*──────────✏️ Editar──────────*/
        private async void EditarCategoriaAsync(CategoriaNodo? nodo)
        {
            if (nodo is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;                    //  🡺  línea 75 resuelta

            string nuevo = await main.DisplayPromptAsync(
                               "Editar Categoría",
                               "Nuevo nombre:",
                               initialValue: nodo.Categoria.Nombre);

            if (string.IsNullOrWhiteSpace(nuevo)) return;

            nodo.Categoria.Nombre = nuevo;
            _repoCategorias.Actualizar(nodo.Categoria);
            Recargar();
        }

        /*──────────🗑️ Eliminar──────────*/
        private async void EliminarCategoriaAsync(CategoriaNodo? nodo)
        {
            if (nodo is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;

            // Asume que tienes acceso al RepositorioProductos
            var repoProductos = new RepositorioProductos();

            var (eliminado, enUso, aEliminar) =
                _repoCategorias.EliminarConSubcategorias(nodo.Categoria.Id, repoProductos);

            if (!eliminado)
            {
                await main.DisplayAlert("No se puede eliminar",
                    "Las siguientes categorías están en uso:\n\n" +
                    string.Join("\n", enUso), "OK");
                return;
            }

            bool confirm = await main.DisplayAlert(
                "Confirmar eliminación",
                "Se eliminarán las siguientes categorías:\n\n" +
                string.Join("\n", aEliminar), "Eliminar", "Cancelar");

            if (!confirm) return;

            // Ya fue eliminado, solo recargar
            Recargar();
        }

    }
}