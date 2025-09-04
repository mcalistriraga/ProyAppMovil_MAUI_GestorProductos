using Microsoft.Maui.Controls;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.ViewModels;
using System.Windows.Input;

namespace MauiAppGestorMovil.Views.Controls
{
    public partial class CategoriaItemSeleccionView : ContentView
    {
        public CategoriaItemSeleccionView()
        {
            InitializeComponent();
        }

        // Propiedad bindable para sangría según nivel
        public static readonly BindableProperty PaddingLeftExtraProperty =
            BindableProperty.Create(
                nameof(PaddingLeftExtra),
                typeof(int),
                typeof(CategoriaItemSeleccionView),
                0);  // valor por defecto

        public int PaddingLeftExtra
        {
            get => (int)GetValue(PaddingLeftExtraProperty);
            set => SetValue(PaddingLeftExtraProperty, value);
        }

        // ✅ Nuevo: propiedad para acceder al comando de selección
        public static readonly BindableProperty CategoriaSeleccionadaCommandProperty =
            BindableProperty.Create(
                nameof(CategoriaSeleccionadaCommand),
                typeof(ICommand),
                typeof(CategoriaItemSeleccionView));

        public ICommand CategoriaSeleccionadaCommand
        {
            get => (ICommand)GetValue(CategoriaSeleccionadaCommandProperty);
            set => SetValue(CategoriaSeleccionadaCommandProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (BindingContext is CategoriaNodo nodo && CategoriaSeleccionadaCommand != null)
            {
                if (CategoriaSeleccionadaCommand.CanExecute(nodo))
                {
                    CategoriaSeleccionadaCommand.Execute(nodo);
                }
            }
        }
    }
}
