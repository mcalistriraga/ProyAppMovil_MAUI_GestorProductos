using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.Helpers;
using MauiAppGestorMovil.ViewModels.Helpers;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MauiAppGestorMovil.Messages;
using System.Threading.Tasks; // para async Task

namespace MauiAppGestorMovil.ViewModels
{
    public class AgregarProductoViewModel : BaseViewModel
    {
        private readonly Categoria categoriaSeleccionada;
        private readonly INavigation navigation;
        private readonly Layout stackPropiedades;
        private readonly RepositorioCategorias repoCategorias = new();
        private readonly RepositorioProductos repoProductos = new();

        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Stock { get; set; } = string.Empty;
        public string Precio { get; set; } = string.Empty;
        public string RutaCategoria { get; }

        // Propiedades específicas
        private Dictionary<string, Entry> entradasPropiedades = new();

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        public AgregarProductoViewModel(Categoria categoria, INavigation nav, Layout stackPropiedades)
        {
            categoriaSeleccionada = categoria;
            navigation = nav;
            this.stackPropiedades = stackPropiedades;

            RutaCategoria = CategoriaHelper.ObtenerRutaCategoriaCompleta(categoriaSeleccionada.Id, repoCategorias);

            GuardarCommand = new Command(async () => await OnGuardar());
            CancelarCommand = new Command(async () =>
            {
                for (int i = navigation.NavigationStack.Count - 2; i >= 0; i--)
                {
                    if (navigation.NavigationStack[i] is MauiAppGestorMovil.Views.SeleccionarCategoriaProducto)
                    {
                        navigation.RemovePage(navigation.NavigationStack[i]);
                    }
                }
                await navigation.PopAsync(); // cierra AgregarProducto
            });

            CargarPropiedadesEspecificas();
        }

        private void CargarPropiedadesEspecificas()
        {
            stackPropiedades.Children.Clear();
            entradasPropiedades.Clear();

            if (categoriaSeleccionada.Propiedades == null || categoriaSeleccionada.Propiedades.Count == 0)
            {
                stackPropiedades.Children.Add(new Label
                {
                    Text = "Sin propiedades específicas definidas para esta categoría.",
                    TextColor = Colors.Gray,
                    FontAttributes = FontAttributes.Italic
                });
                return;
            }

            foreach (var nombrePropiedad in categoriaSeleccionada.Propiedades)
            {
                stackPropiedades.Children.Add(new Label
                {
                    Text = nombrePropiedad + ":",
                    TextColor = Colors.Black
                });

                var entry = new Entry
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black
                };

                stackPropiedades.Children.Add(entry);
                entradasPropiedades[nombrePropiedad] = entry;
            }
        }

        private async Task OnGuardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Stock) ||
                string.IsNullOrWhiteSpace(Precio))
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor complete todos los campos requeridos.", "OK");
                }
                return;
            }

            // Validar nombre único en toda la base
            if (repoProductos.ExistePorNombre(Nombre))
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Nombre duplicado",
                        "Ya existe un producto con ese nombre. Por favor, ingrese un nombre diferente.",
                        "OK");
                }
                return;
            }

            int.TryParse(Stock, out int cantidad);
            decimal.TryParse(Precio, out decimal precioDecimal);

            Dictionary<string, string> propiedades = new();
            foreach (var kvp in entradasPropiedades)
            {
                string valor = (kvp.Value != null && !string.IsNullOrWhiteSpace(kvp.Value.Text))
                    ? kvp.Value.Text
                    : "N/A";
                propiedades[kvp.Key] = valor;
            }

            var nuevoProducto = new Producto
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Stock = cantidad,
                Precio = precioDecimal,
                IdCategoria = categoriaSeleccionada.Id,
                CategoriaNombre = categoriaSeleccionada.Nombre,
                PropiedadesEspecificas = propiedades
            };

            repoProductos.Agregar(nuevoProducto);

            // Mensaje de éxito
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Producto agregado correctamente.", "OK");
            }

            // Notificar para recargar lista de productos
            WeakReferenceMessenger.Default.Send(new RecargarProductosMessage(true));

            // Eliminar SeleccionarCategoriaProducto.xaml de la pila de navegación
            for (int i = navigation.NavigationStack.Count - 2; i >= 0; i--)
            {
                if (navigation.NavigationStack[i] is MauiAppGestorMovil.Views.SeleccionarCategoriaProducto)
                {
                    navigation.RemovePage(navigation.NavigationStack[i]);
                }
            }

            // Cerrar AgregarProducto y volver a GestionDeProductos.xaml
            await navigation.PopAsync();
        }
    }
}
