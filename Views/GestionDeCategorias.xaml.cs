using System;
using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Helpers;     // ⬅️  NUEVO
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
        }

        /*───────────────────────────
         *  Cerrar teclado al tocar fondo
         *──────────────────────────*/
        private void OnFondoTocado(object sender, EventArgs e)
        {
            CloseTecladoHelper.Ocultar();
        }

        /*───────────────────────────
         *  AGREGAR CATEGORÍA (ContentPage)
         *──────────────────────────*/
        private async void AgregarCategoria_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarCategoria(_repoCategorias));
        }

        /*───────────────────────────
         *  AGREGAR SUBCATEGORÍA (popup temporal)
         *──────────────────────────*/
        private async void AgregarSubcategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is CategoriaNodo padre)
            {
                string nombre = await DisplayPromptAsync("Nueva Subcategoría",
                                  $"Ingrese el nombre para la subcategoría de '{padre.Categoria.Nombre}':");

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    var nuevaSub = new Categoria
                    {
                        Id = _repoCategorias.GenerarNuevoId(),
                        Nombre = nombre,
                        IdPadre = padre.Categoria.Id,
                        Propiedades = new()
                    };

                    _repoCategorias.Agregar(nuevaSub);
                    _viewModel.Recargar();
                }
            }
        }

        /*───────────────────────────
         *  EDITAR
         *──────────────────────────*/
        private async void EditarCategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is CategoriaNodo nodo)
            {
                string nuevo = await DisplayPromptAsync("Editar Categoría",
                                                         "Nuevo nombre:",
                                                         initialValue: nodo.Categoria.Nombre);

                if (!string.IsNullOrWhiteSpace(nuevo))
                {
                    nodo.Categoria.Nombre = nuevo;
                    _repoCategorias.Actualizar(nodo.Categoria);
                    _viewModel.Recargar();
                }
            }
        }

        /*───────────────────────────
         *  ELIMINAR
         *──────────────────────────*/
        private async void EliminarCategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is CategoriaNodo nodo)
            {
                bool ok = await DisplayAlert("Eliminar",
                            $"¿Eliminar categoría '{nodo.Categoria.Nombre}'?", "Sí", "No");

                if (ok)
                {
                    bool eliminada = _repoCategorias.Eliminar(nodo.Categoria.Id);

                    if (!eliminada)
                        await DisplayAlert("Error", "No se puede eliminar porque tiene subcategorías.", "OK");

                    _viewModel.Recargar();
                }
            }
        }

        /*───────────────────────────
         *  REFRESCO AL VOLVER
         *──────────────────────────*/
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Recargar();   // Refresca si venimos de Agregar/Editar
        }
    }
}
