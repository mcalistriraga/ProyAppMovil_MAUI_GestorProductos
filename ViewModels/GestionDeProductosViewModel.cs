using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System;
using System.Threading.Tasks;

namespace MauiAppGestorMovil.ViewModels
{
    public partial class GestionDeProductosViewModel : BindableObject
    {
        // Colección observable para la interfaz de usuario
        public ObservableCollection<Producto> Productos { get; set; }

        // Comandos expuestos a la vista
        public ICommand AgregarProductoCommand { get; }
        public ICommand VerProductoCommand { get; }
        public ICommand EditarProductoCommand { get; }
        public ICommand EliminarProductoCommand { get; }

        // Para mostrar mensajes desde el ViewModel
        public Func<string, string, Task>? MostrarMensaje { get; set; }

        private int proximoId = 1;

        private RepositorioProductos repositorioProductos;
        private RepositorioCategorias repositorioCategorias;

        public GestionDeProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>();

            AgregarProductoCommand = new Command(async () => await AgregarProducto());
            VerProductoCommand = new Command<Producto>(VerProducto);
            EditarProductoCommand = new Command<Producto>(EditarProducto);
            EliminarProductoCommand = new Command<Producto>(EliminarProducto);

            repositorioProductos = new RepositorioProductos();
            repositorioCategorias = new RepositorioCategorias();

            CargarProductos();
        }

        private void CargarProductos()
        {
            var productosDesdeArchivo = repositorioProductos.ObtenerTodos();

            foreach (var producto in productosDesdeArchivo)
            {
                var categoria = repositorioCategorias.BuscarPorId(producto.IdCategoria);
                producto.CategoriaNombre = categoria?.Nombre ?? "Sin categoría";

                Productos.Add(producto);

                if (producto.Id >= proximoId)
                {
                    proximoId = producto.Id + 1;
                }
            }
        }

        private async Task AgregarProducto()
        {
            var nuevoProducto = new Producto
            {
                Id = proximoId++,
                Nombre = $"Producto #{proximoId}",
                IdCategoria = 1,
                Precio = 9.99m,
                Stock = 100,
                Descripcion = "Producto de prueba",
                CategoriaNombre = "Electrónica"
            };

            Productos.Add(nuevoProducto);
            repositorioProductos.Agregar(nuevoProducto);

            if (MostrarMensaje != null)
            {
                await MostrarMensaje.Invoke("Producto Agregado", $"Producto '{nuevoProducto.Nombre}' agregado.");
            }
        }

        private async void VerProducto(Producto producto)
        {
            await Application.Current!.MainPage!.Navigation!.PushAsync(new Views.DetallesDelProducto(producto));
        }

        private void EditarProducto(Producto producto)
        {
            MostrarMensaje?.Invoke("Editar", $"Editar producto '{producto.Nombre}' en desarrollo.");
        }

        private async void EliminarProducto(Producto producto)
        {
            if (producto == null)
                return;

            bool confirmado = await Application.Current.MainPage.DisplayAlert(
                "Confirmar eliminación",
                $"¿Deseas eliminar el producto:\n\n'{producto.Nombre}'?",
                "Sí", "Cancelar");

            if (!confirmado)
                return;

            try
            {
                // 1. Eliminar del archivo
                repositorioProductos.Eliminar(producto.Id);

                // 2. Actualizar la colección
                var nuevaLista = repositorioProductos.ObtenerTodos();

                // 3. Refrescar UI en el hilo principal
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Productos.Clear();

                    foreach (var prod in nuevaLista)
                    {
                        var cat = repositorioCategorias.BuscarPorId(prod.IdCategoria);
                        prod.CategoriaNombre = cat?.Nombre ?? "Sin categoría";
                        Productos.Add(prod);
                    }
                });

                // 4. Mostrar mensaje de éxito
                await MostrarMensaje?.Invoke("Producto eliminado", $"'{producto.Nombre}' fue eliminado correctamente.");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al eliminar:\n{ex.Message}", "OK");
            }
        }
       
    }
}
