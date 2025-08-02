using MauiAppGestorMovil.Helpers;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;
using MauiAppGestorMovil.ViewModels;


namespace MauiAppGestorMovil.Views
{
    public partial class EditarCategoria : ContentPage
    {
        public EditarCategoria(Categoria categoria)
        {
            InitializeComponent();
            ICategoriaService categoriaService = new CategoriaService();
            BindingContext = new EditarCategoriaViewModel(categoria, categoriaService, Navigation);
        }

        private void OnFondoTocado(object sender, EventArgs e)
        {
            CloseTecladoHelper.Ocultar();
        }


    }
}