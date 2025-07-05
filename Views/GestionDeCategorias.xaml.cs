using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels;
using System;
using System.Linq;

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
            _viewModel = new GestionDeCategoriasViewModel(_repoCategorias); // pasamos el repo
            BindingContext = _viewModel;
        }

        private async void AgregarCategoria_Clicked(object sender, EventArgs e)
        {
            string nombre = await DisplayPromptAsync("Nueva Categoría", "Ingrese el nombre de la categoría:");
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                var nuevaCategoria = new Categoria
                {
                    Id = _repoCategorias.GenerarNuevoId(),
                    Nombre = nombre,
                    IdPadre = null,
                    Propiedades = new List<string>()
                };

                _repoCategorias.Agregar(nuevaCategoria);
                _viewModel.Recargar(); // actualiza jerarquía
            }
        }

        private async void AgregarSubcategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is CategoriaNodo padre)
            {
                string nombre = await DisplayPromptAsync("Nueva Subcategoría", $"Ingrese el nombre para la subcategoría de '{padre.Categoria.Nombre}':");

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    var nuevaSubcategoria = new Categoria
                    {
                        Id = _repoCategorias.GenerarNuevoId(),
                        Nombre = nombre,
                        IdPadre = padre.Categoria.Id,
                        Propiedades = new List<string>()
                    };

                    _repoCategorias.Agregar(nuevaSubcategoria);
                    _viewModel.Recargar();
                }
            }
        }

        private async void EditarCategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is CategoriaNodo categoriaNodo)
            {
                string nuevoNombre = await DisplayPromptAsync("Editar Categoría", "Nuevo nombre:", initialValue: categoriaNodo.Categoria.Nombre);
                if (!string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    categoriaNodo.Categoria.Nombre = nuevoNombre;
                    _repoCategorias.Actualizar(categoriaNodo.Categoria);
                    _viewModel.Recargar();
                }
            }
        }

        private async void EliminarCategoria_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is CategoriaNodo categoriaNodo)
            {
                bool confirmar = await DisplayAlert("Eliminar", $"¿Eliminar categoría '{categoriaNodo.Categoria.Nombre}'?", "Sí", "No");
                if (confirmar)
                {
                    bool eliminada = _repoCategorias.Eliminar(categoriaNodo.Categoria.Id);
                    if (!eliminada)
                    {
                        await DisplayAlert("Error", "No se puede eliminar porque tiene subcategorías.", "OK");
                    }
                    else
                    {
                        _viewModel.Recargar();
                    }
                }
            }
        }
    }
}
