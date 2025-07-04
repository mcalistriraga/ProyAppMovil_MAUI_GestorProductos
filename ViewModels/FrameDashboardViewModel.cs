using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System;

namespace MauiAppGestorMovil.ViewModels
{
    public class DashboardOption
    {
        public string Title { get; set; } = "";
        public ICommand Command { get; set; } = null!;
        public Color ButtonColor { get; set; } = Colors.Transparent;
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
                    Title = "Productos",
                    ButtonColor = Color.FromArgb("#FF9800"),
                    Command = new Command(async () =>
                        await Application.Current!.MainPage!.Navigation!.PushAsync(new Views.GestionDeProductos()))
                },
                new DashboardOption
                {
                    Title = "Categorías",
                    ButtonColor = Color.FromArgb("#2196F3"),
                    Command = new Command(async () =>
                        await Application.Current!.MainPage!.Navigation!.PushAsync(new Views.GestionDeCategorias()))
                },
                new DashboardOption
                {
                    Title = "Buscar",
                    ButtonColor = Color.FromArgb("#4CAF50"),
                    Command = new Command(() =>
                        Application.Current!.MainPage!.DisplayAlert("Buscar", "Función Buscar en desarrollo", "OK"))
                }
            };
        }
    }
}
