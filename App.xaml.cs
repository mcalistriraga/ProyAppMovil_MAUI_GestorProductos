using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Views;
using MauiAppGestorMovil.Services;


namespace MauiAppGestorMovil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InicializadorDatos.InicializarArchivos();

            // Envolvemos el dashboard en una NavigationPage para habilitar PushAsync
            MainPage = new NavigationPage(new FrameDashboard());
        }
       
    }
}
