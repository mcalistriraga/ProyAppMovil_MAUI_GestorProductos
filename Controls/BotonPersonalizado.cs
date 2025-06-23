using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace MauiAppGestorMovil.Controls
{
    public class BotonPersonalizado : Button
    {
        public BotonPersonalizado()
        {
            // Asignamos los bindings directamente a las propiedades de Button
            this.SetBinding(TextProperty, new Binding(nameof(Texto), source: this));
            this.SetBinding(BackgroundColorProperty, new Binding(nameof(ColorFondo), source: this));
            this.SetBinding(WidthRequestProperty, new Binding(nameof(Ancho), source: this));
            this.SetBinding(HeightRequestProperty, new Binding(nameof(Alto), source: this));
            this.SetBinding(FontSizeProperty, new Binding(nameof(TamanoLetra), source: this));
            this.SetBinding(CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            this.SetBinding(CommandProperty, new Binding(nameof(Comando), source: this));
        }

        // Texto del botón
        public static readonly BindableProperty TextoProperty =
            BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotonPersonalizado), default(string));

        public string Texto
        {
            get => (string)GetValue(TextoProperty);
            set => SetValue(TextoProperty, value);
        }

        // Color de fondo
        public static readonly BindableProperty ColorFondoProperty =
            BindableProperty.Create(nameof(ColorFondo), typeof(Color), typeof(BotonPersonalizado), Colors.Blue);

        public Color ColorFondo
        {
            get => (Color)GetValue(ColorFondoProperty);
            set => SetValue(ColorFondoProperty, value);
        }

        // Ancho
        public static readonly BindableProperty AnchoProperty =
            BindableProperty.Create(nameof(Ancho), typeof(double), typeof(BotonPersonalizado), 100.0);

        public double Ancho
        {
            get => (double)GetValue(AnchoProperty);
            set => SetValue(AnchoProperty, value);
        }

        // Alto
        public static readonly BindableProperty AltoProperty =
            BindableProperty.Create(nameof(Alto), typeof(double), typeof(BotonPersonalizado), 40.0);

        public double Alto
        {
            get => (double)GetValue(AltoProperty);
            set => SetValue(AltoProperty, value);
        }

        // Tamaño de letra
        public static readonly BindableProperty TamanoLetraProperty =
            BindableProperty.Create(nameof(TamanoLetra), typeof(double), typeof(BotonPersonalizado), 14.0);

        public double TamanoLetra
        {
            get => (double)GetValue(TamanoLetraProperty);
            set => SetValue(TamanoLetraProperty, value);
        }

        // Esquinas redondeadas (evita warning CS0108)
        public static readonly new BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(BotonPersonalizado), 10);

        public new int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Comando para soporte MVVM (evita warning CS0108)
        public static readonly new BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Comando), typeof(ICommand), typeof(BotonPersonalizado), null);

        public new ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        // Propiedad pública "Comando" para mantener compatibilidad con la app
        public ICommand Comando
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
