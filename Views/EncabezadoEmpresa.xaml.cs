using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views
{
    public partial class EncabezadoEmpresa : ContentView
    {
        public static readonly BindableProperty TituloPaginaProperty =
            BindableProperty.Create(nameof(TituloPagina), typeof(string), typeof(EncabezadoEmpresa), default(string));

        public string TituloPagina
        {
            get => (string)GetValue(TituloPaginaProperty);
            set => SetValue(TituloPaginaProperty, value);
        }

        public EncabezadoEmpresa()
        {
            InitializeComponent();

            // Agregar gesto táctil al logo
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                var currentPage = Application.Current?.MainPage?.Navigation?.NavigationStack?.LastOrDefault();

                // Solo navegar si NO estamos ya en FrameDashboard
                if (currentPage is not FrameDashboard)
                {
                    await Application.Current!.MainPage!.Navigation!.PushAsync(new FrameDashboard());
                }
            };

            LogoEmpresa.GestureRecognizers.Add(tapGesture);
        }
    }
}
