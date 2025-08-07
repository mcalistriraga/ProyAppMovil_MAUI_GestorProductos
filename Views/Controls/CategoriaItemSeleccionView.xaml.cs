using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views.Controls
{
    public partial class CategoriaItemSeleccionView : ContentView
    {
        public CategoriaItemSeleccionView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty PaddingLeftExtraProperty =
            BindableProperty.Create(
                nameof(PaddingLeftExtra),
                typeof(int),
                typeof(CategoriaItemSeleccionView),
                0);

        public int PaddingLeftExtra
        {
            get => (int)GetValue(PaddingLeftExtraProperty);
            set => SetValue(PaddingLeftExtraProperty, value);
        }
    }
}
