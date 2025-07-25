using System;
using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Helpers;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels;

namespace MauiAppGestorMovil.Views
{
    public partial class GestionDeCategorias : ContentPage
    {
        private readonly RepositorioCategorias _repoCategorias;
        private readonly GestionDeCategoriasViewModel _viewModel;

        public GestionDeCategorias()
        {
            InitializeComponent();

            _repoCategorias = new RepositorioCategorias();
            _viewModel = new GestionDeCategoriasViewModel(_repoCategorias);

            BindingContext = _viewModel;

            // Suscribirse al mensaje para recargar automáticamente al volver de una edición
            MessagingCenter.Subscribe<object>(this, "RecargarCategorias", (_) =>
            {
                _viewModel.Recargar();
            });
        }

        /*──────────────────────────────*/
        private void OnFondoTocado(object sender, EventArgs e) =>
            CloseTecladoHelper.Ocultar();

        /*──────────────────────────────*/
        private async void AgregarCategoria_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new AgregarCategoria(_repoCategorias));

        /*──────────────────────────────*/
        private async void AgregarSubcategoria_Clicked(object sender, EventArgs e)
        {
            CategoriaNodo? padre = null;

            if (sender is Button b && b.CommandParameter is CategoriaNodo n1) padre = n1;
            if (sender is ImageButton ib && ib.CommandParameter is CategoriaNodo n2) padre = n2;

            if (padre == null) return;

            await Navigation.PushModalAsync(new AgregarSubcategoria(_repoCategorias, padre));
        }

        /*──────────────────────────────*/
        private async void EliminarCategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton ib && ib.CommandParameter is CategoriaNodo nodo)
            {
                bool ok = await DisplayAlert("Eliminar",
                                             $"¿Eliminar categoría '{nodo.Categoria.Nombre}'?",
                                             "Sí", "No");

                if (ok)
                {
                    bool elim = _repoCategorias.Eliminar(nodo.Categoria.Id);
                    if (!elim)
                        await DisplayAlert("Error", "No se puede eliminar porque tiene subcategorías.", "OK");

                    _viewModel.Recargar();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Recargar();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Cancelar la suscripción para evitar fugas de memoria
            MessagingCenter.Unsubscribe<object>(this, "RecargarCategorias");
        }
    }
}
