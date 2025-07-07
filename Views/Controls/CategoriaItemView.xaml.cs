using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views.Controls
{
    public partial class CategoriaItemView : ContentView
    {
        public CategoriaItemView()
        {
            InitializeComponent();
        }

        // Definición de la propiedad bindable PaddingLeftExtra (int)
        public static readonly BindableProperty PaddingLeftExtraProperty =
            BindableProperty.Create(
                nameof(PaddingLeftExtra),
                typeof(int),
                typeof(CategoriaItemView),
                0);  // valor por defecto

        public int PaddingLeftExtra
        {
            get => (int)GetValue(PaddingLeftExtraProperty);
            set => SetValue(PaddingLeftExtraProperty, value);
        }
    }
}
