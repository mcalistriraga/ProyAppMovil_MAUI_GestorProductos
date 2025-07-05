using Microsoft.Maui.Controls;
using System;
using MauiAppGestorMovil.ViewModels;

namespace MauiAppGestorMovil.Views
{
    public partial class GestionDeCategorias : ContentPage
    {
        public GestionDeCategorias()
        {
            InitializeComponent();
            BindingContext = new GestionDeCategoriasViewModel(); // Enlazamos el ViewModel jerárquico
        }

        private async void AgregarSubcategoria_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Agregar Subcategoría", "Funcionalidad en desarrollo", "OK");
            // Aquí podrías abrir una página o mostrar un formulario en el futuro
        }

        private async void EditarCategoria_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Editar Categoría", "Funcionalidad en desarrollo", "OK");
            // Aquí se podría navegar a una página de edición más adelante
        }

        private async void EliminarCategoria_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Eliminar Categoría", "¿Estás seguro que deseas eliminar esta categoría?", "Sí", "No");
            if (confirm)
            {
                await DisplayAlert("Eliminado", "Categoría eliminada (simulado)", "OK");
                // Aquí deberías invocar un método del ViewModel para eliminarla de la colección
            }
        }
    }
}
