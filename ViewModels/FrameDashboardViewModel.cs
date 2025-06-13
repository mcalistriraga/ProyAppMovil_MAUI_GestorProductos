using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System;

namespace MauiAppGestorMovil.ViewModels
{
    public class DashboardOption
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; set; }
        public Color ButtonColor { get; set; } // Nueva propiedad para el color del botón
    }

    public class FrameDashboardViewModel
    {
        public ObservableCollection<DashboardOption> DashboardOptions { get; set; }

        public FrameDashboardViewModel()
        {
            DashboardOptions = new ObservableCollection<DashboardOption>
            {
                new DashboardOption
                {
                    Title = "Buscar",
                    Icon = "buscar.png",
                    ButtonColor = Color.FromArgb("#4CAF50"),
                    Command = new Command(() =>
                        Application.Current.MainPage.DisplayAlert("Buscar", "Función Buscar en desarrollo", "OK"))
                },
                new DashboardOption
                {
                    Title = "Categorías",
                    Icon = "categorias.png",
                    ButtonColor = Color.FromArgb("#2196F3"),
                    Command = new Command(() =>
                        Application.Current.MainPage.DisplayAlert("Categorías", "Función Categorías en desarrollo", "OK"))
                },
                new DashboardOption
                {
                    Title = "Productos",
                    Icon = "productos.png",
                    ButtonColor = Color.FromArgb("#FF9800"),
                    Command = new Command(async () =>
                        await Application.Current.MainPage.Navigation.PushAsync(new Views.GestionDeProductos()))
                }
            };
        }
    }
}
