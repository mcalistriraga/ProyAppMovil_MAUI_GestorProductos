using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Esperamos un momento a que termine de cargarse si es necesario
            await Task.Delay(300); // ajusta si tu carga es más lenta

            var total = _viewModel.Productos.Count;
            var ultimo = _viewModel.Productos.LastOrDefault()?.Nombre ?? "N/A";

            await DisplayAlert("Productos cargados",
                $"Cantidad total: {total}\nÚltimo producto: {ultimo}",
                "OK");
        }
    }
}
