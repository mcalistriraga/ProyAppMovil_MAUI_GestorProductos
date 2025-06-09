using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace MauiAppGestorMovil.Views
{
    public partial class CargandoApp : ContentPage
    {
        public CargandoApp()
        {
            InitializeComponent();
            IniciarTemporizador();
        }

        private async void IniciarTemporizador()
        {
            await Task.Delay(1000); // espera de 1 segundo

            // Navegar a la próxima página (por ejemplo, LoginApp)
            //Application.Current.MainPage = new LoginApp(); // Asegúrate de que LoginApp esté implementada
            Application.Current!.MainPage = new FrameDashboard();   // Asegúrate de que LoginApp esté implementada
        }
    }
}
