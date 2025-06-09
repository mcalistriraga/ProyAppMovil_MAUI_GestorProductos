using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Views;

namespace MauiAppGestorMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Envolvemos el dashboard en una NavigationPage para habilitar PushAsync
            MainPage = new NavigationPage(new FrameDashboard());
        }

    }
}
