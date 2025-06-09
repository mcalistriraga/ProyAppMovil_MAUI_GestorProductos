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
        }

        private void btnAgregarProducto_Clicked(object sender, EventArgs e)
        {
            _viewModel.AgregarProductoCommand.Execute(null);
        }
    }
}
