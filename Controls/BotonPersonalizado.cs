using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace MauiAppGestorMovil.Controls
{
    public class BotonPersonalizado : Button
    {
        public BotonPersonalizado()
        {
            this.SetBinding(TextProperty, new Binding(nameof(Texto), source: this));
            this.SetBinding(BackgroundColorProperty, new Binding(nameof(ColorFondo), source: this));
            this.SetBinding(WidthRequestProperty, new Binding(nameof(Ancho), source: this));
            this.SetBinding(HeightRequestProperty, new Binding(nameof(Alto), source: this));
            this.SetBinding(FontSizeProperty, new Binding(nameof(TamanoLetra), source: this));
            this.SetBinding(CornerRadiusProperty, new Binding(nameof(CornerRadiusPersonalizado), source: this));
        }

        public static readonly BindableProperty TextoProperty =
            BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotonPersonalizado), default(string));

        public string Texto
        {
            get => (string)GetValue(TextoProperty);
            set => SetValue(TextoProperty, value);
        }

        public static readonly BindableProperty ColorFondoProperty =
            BindableProperty.Create(nameof(ColorFondo), typeof(Color), typeof(BotonPersonalizado), Colors.Blue);

        public Color ColorFondo
        {
            get => (Color)GetValue(ColorFondoProperty);
            set => SetValue(ColorFondoProperty, value);
        }

        public static readonly BindableProperty AnchoProperty =
            BindableProperty.Create(nameof(Ancho), typeof(double), typeof(BotonPersonalizado), 100.0);

        public double Ancho
        {
            get => (double)GetValue(AnchoProperty);
            set => SetValue(AnchoProperty, value);
        }

        public static readonly BindableProperty AltoProperty =
            BindableProperty.Create(nameof(Alto), typeof(double), typeof(BotonPersonalizado), 40.0);

        public double Alto
        {
            get => (double)GetValue(AltoProperty);
            set => SetValue(AltoProperty, value);
        }

        public static readonly BindableProperty TamanoLetraProperty =
            BindableProperty.Create(nameof(TamanoLetra), typeof(double), typeof(BotonPersonalizado), 14.0);

        public double TamanoLetra
        {
            get => (double)GetValue(TamanoLetraProperty);
            set => SetValue(TamanoLetraProperty, value);
        }

        // Esquinas redondeadas personalizadas (evita conflicto con Button.CornerRadius)
        public static readonly BindableProperty CornerRadiusPersonalizadoProperty =
            BindableProperty.Create(nameof(CornerRadiusPersonalizado), typeof(int), typeof(BotonPersonalizado), 10);

        public int CornerRadiusPersonalizado
        {
            get => (int)GetValue(CornerRadiusPersonalizadoProperty);
            set => SetValue(CornerRadiusPersonalizadoProperty, value);
        }
    }
}
