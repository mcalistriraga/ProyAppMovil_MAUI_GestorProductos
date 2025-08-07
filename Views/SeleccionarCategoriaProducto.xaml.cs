using MauiAppGestorMovil.ViewModels;

namespace MauiAppGestorMovil.Views
{
    public partial class SeleccionarCategoriaProducto : ContentPage
    {
        public SeleccionarCategoriaProducto()
        {
            InitializeComponent();
            BindingContext = new SeleccionarCategoriaProductoViewModel();
        }
    }
}
