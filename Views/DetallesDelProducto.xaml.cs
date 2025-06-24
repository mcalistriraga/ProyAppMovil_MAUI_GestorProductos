using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.Helpers;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views
{
    public partial class DetallesDelProducto : ContentPage
    {
        public DetallesDelProducto(Producto producto)
        {
            InitializeComponent();

            var repoCategorias = new RepositorioCategorias();
            var rutaCompleta = CategoriaHelper.ObtenerRutaCategoriaCompleta(producto.IdCategoria, repoCategorias);

            // Usamos un ViewModel anónimo para binding (o crear un DTO si quieres)
            var vm = new
            {
                producto.Id,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio,
                producto.Stock,
                producto.IdCategoria,
                producto.CategoriaNombre,
                CategoriaNombreCompleta = rutaCompleta
            };

            BindingContext = vm;
        }

        private async void BtnCerrar_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
