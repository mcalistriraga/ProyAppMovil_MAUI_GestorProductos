using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views
{
    public partial class EditarCategoria : ContentPage
    {
        public EditarCategoria(RepositorioCategorias repo, Categoria categoria)
        {
            InitializeComponent();

            BindingContext = new EditarCategoriaViewModel(categoria, repo, Navigation);
        }
    }
}
