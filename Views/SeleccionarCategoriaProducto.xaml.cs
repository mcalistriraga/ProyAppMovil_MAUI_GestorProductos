using Microsoft.Maui.Controls;
using MauiAppGestorMovil.ViewModels;

namespace MauiAppGestorMovil.Views
{
    public partial class SeleccionarCategoriaProducto : ContentPage
    {
        private readonly SeleccionarCategoriaProductoViewModel _viewModel;

        public SeleccionarCategoriaProducto()
        {
            InitializeComponent();

            // ✅ Establecer BindingContext
            _viewModel = new SeleccionarCategoriaProductoViewModel();
            BindingContext = _viewModel;
        }
    }
}
