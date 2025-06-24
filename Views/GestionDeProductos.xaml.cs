using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace MauiAppGestorMovil.Views
{
    public partial class GestionDeProductos : ContentPage
    {
        private readonly GestionDeProductosViewModel _viewModel = new GestionDeProductosViewModel();

        public GestionDeProductos()
        {
            InitializeComponent();

            _viewModel.MostrarMensaje = async (titulo, mensaje) =>
            {
                await DisplayAlert(titulo, mensaje, "OK");
            };

            BindingContext = _viewModel;
        }

        private async void MostrarRutaDePersistencia()
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            await DisplayAlert("Ruta de persistencia actual", ruta, "OK");
        }
    }
}
