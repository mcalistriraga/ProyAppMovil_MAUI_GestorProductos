using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace MauiAppGestorMovil.Views
{
    public partial class GestionDeProductos : ContentPage
    {
        private GestionDeProductosViewModel _viewModel;

        public GestionDeProductos()
        {
            InitializeComponent();

            _viewModel = new GestionDeProductosViewModel();

            _viewModel.MostrarMensaje = async (titulo, mensaje) =>
            {
                await DisplayAlert(titulo, mensaje, "OK");
            };

            BindingContext = _viewModel;

            // Mostrar la ruta usada para persistencia
            //  MostrarRutaDePersistencia();  // Activar solo para verificar el path de persistencia
        }

        private async void MostrarRutaDePersistencia()
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            await DisplayAlert("Ruta de persistencia actual", ruta, "OK");
        }

        private void btnAgregarProducto_Clicked(object sender, EventArgs e)
        {
            _viewModel.AgregarProductoCommand.Execute(null);
        }

        private void btnVerProducto_Clicked(object sender, EventArgs e)
        {
            // TODO: Implementar navegación a página de detalles
            DisplayAlert("Ver", "Funcionalidad de 'Ver producto' en desarrollo.", "OK");
        }

        private void btnEditarProducto_Clicked(object sender, EventArgs e)
        {
            // TODO: Implementar navegación a edición
            DisplayAlert("Editar", "Funcionalidad de 'Editar producto' en desarrollo.", "OK");
        }

        private void btnEliminarProducto_Clicked(object sender, EventArgs e)
        {
            // TODO: Implementar eliminación del producto con confirmación
            DisplayAlert("Eliminar", "Funcionalidad de 'Eliminar producto' en desarrollo.", "OK");
        }

    }
}
