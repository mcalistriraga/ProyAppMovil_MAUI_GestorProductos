using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views
{
    public partial class AgregarProducto : ContentPage
    {
        public AgregarProducto(Categoria categoriaSeleccionada)
        {
            InitializeComponent();
            var vm = new AgregarProductoViewModel(categoriaSeleccionada, Navigation, stackPropiedades);
            BindingContext = vm;
        }

        private void OnFondoTocado(object sender, EventArgs e)
        {
            Helpers.CloseTecladoHelper.Ocultar();
        }
    }
}