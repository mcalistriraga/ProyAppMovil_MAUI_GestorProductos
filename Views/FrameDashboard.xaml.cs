using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using MauiAppGestorMovil.Views;

namespace MauiAppGestorMovil.Views
{
    public partial class FrameDashboard : ContentPage
    {
        public FrameDashboard()
        {
            InitializeComponent();
        }

        private void OnBuscarClicked(object sender, EventArgs e)
        {
            // Aquí puedes navegar a la vista de búsqueda o mostrar un mensaje
            DisplayAlert("Buscar", "Función Buscar en desarrollo", "OK");
        }

        private void OnCategoriasClicked(object sender, EventArgs e)
        {
            // Aquí puedes navegar a la vista de categorías o mostrar un mensaje
            DisplayAlert("Categorías", "Función Categorías en desarrollo", "OK");
        }

        private async void OnProductosClicked(object sender, EventArgs e)
        {
            // Navegamos a la pantalla de gestión de productos
            await Navigation.PushAsync(new GestionDeProductos());
        }
    }
}
