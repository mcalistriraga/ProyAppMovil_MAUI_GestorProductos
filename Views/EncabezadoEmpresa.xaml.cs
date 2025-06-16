namespace MauiAppGestorMovil.Views;

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
    }
}
