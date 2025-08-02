using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels.Helpers;
using MauiAppGestorMovil.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MauiAppGestorMovil.Messages;

namespace MauiAppGestorMovil.ViewModels
{
    public class GestionDeCategoriasViewModel : BaseViewModel
    {
        private readonly RepositorioCategorias _repoCategorias;

        public ObservableCollection<CategoriaNodo> CategoriasJerarquicas { get; } = new();

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

            // Suscribirse al mensaje para recargar cuando se edite una categoría
            WeakReferenceMessenger.Default.Register<GestionDeCategoriasViewModel, CategoriaEditadaMessage>(this, (r, m) =>
            {
                if (m.Value)
                    r.Recargar();
            });
        }

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

        private void AgregarSubcategoriaAsync(CategoriaNodo? padre)
        {
            if (padre is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;

            var page = new AgregarSubcategoria(_repoCategorias, padre);
            main.Navigation.PushModalAsync(page)
                .ContinueWith(_ => MainThread.BeginInvokeOnMainThread(() => Recargar()));
        }

        private void EditarCategoriaAsync(CategoriaNodo? nodo)
        {
            if (nodo is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;

            var page = new EditarCategoria(nodo.Categoria); // <- CORREGIDO
            main.Navigation.PushModalAsync(page)
                .ContinueWith(_ => MainThread.BeginInvokeOnMainThread(() => Recargar()));
        }

        private async void EliminarCategoriaAsync(CategoriaNodo? nodo)
        {
            if (nodo is null) return;

            var main = Application.Current?.MainPage;
            if (main is null) return;

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

            Recargar();
        }
    }
}
