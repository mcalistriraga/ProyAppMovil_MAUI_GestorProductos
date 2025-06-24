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
        // Colección observable para notificar la vista cuando cambia la lista de productos
        public ObservableCollection<Producto> Productos { get; set; }

        // Comandos para las acciones de agregar, ver, editar y eliminar productos
        public ICommand AgregarProductoCommand { get; }
        public ICommand VerProductoCommand { get; }
        public ICommand EditarProductoCommand { get; }
        public ICommand EliminarProductoCommand { get; }

        // Delegado para mostrar mensajes en la vista (por ejemplo, alertas)
        public Func<string, string, Task>? MostrarMensaje { get; set; }

        // Variable para llevar el control del próximo Id a asignar al agregar un producto nuevo
        private int proximoId = 1;

        // Repositorios para manejar persistencia de productos y categorías
        private RepositorioProductos repositorioProductos;
        private RepositorioCategorias repositorioCategorias;

        // Constructor: inicializa propiedades, comandos y carga los productos existentes
        public GestionDeProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>();

            // Inicialización de comandos con métodos asociados
            AgregarProductoCommand = new Command(async () => await AgregarProducto());
            VerProductoCommand = new Command<Producto>(VerProducto);
            EditarProductoCommand = new Command<Producto>(EditarProducto);
            EliminarProductoCommand = new Command<Producto>(EliminarProducto);

            repositorioProductos = new RepositorioProductos();
            repositorioCategorias = new RepositorioCategorias();

            CargarProductos();
        }

        // Carga los productos desde el repositorio y actualiza la lista observable
        private void CargarProductos()
        {
            var productosDesdeArchivo = repositorioProductos.ObtenerTodos();

            foreach (var producto in productosDesdeArchivo)
            {
                // Obtiene el nombre de la categoría asociada para mostrar
                var categoria = repositorioCategorias.BuscarPorId(producto.IdCategoria);
                producto.CategoriaNombre = categoria?.Nombre ?? "Sin categoría";

                Productos.Add(producto);

                // Actualiza el próximo Id para nuevos productos
                if (producto.Id >= proximoId)
                {
                    proximoId = producto.Id + 1;
                }
            }
        }

        // Método para agregar un producto nuevo (actualmente con valores fijos de ejemplo)
        private async Task AgregarProducto()
        {
            var nuevoProducto = new Producto
            {
                Id = proximoId++,
                Nombre = $"Producto #{proximoId}",
                IdCategoria = 1, // Asignar categoría por defecto (se puede mejorar)
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

        // Método para ver detalles del producto 
        private async void VerProducto(Producto producto)
        {
            await Application.Current!.MainPage!.Navigation!.PushAsync(new Views.DetallesDelProducto(producto));
        }


        // Método para editar el producto (por implementar navegación o lógica real)
        private void EditarProducto(Producto producto)
        {
            MostrarMensaje?.Invoke("Editar", $"Editar producto '{producto.Nombre}' en desarrollo.");
        }

        // Método para eliminar producto con confirmación al usuario
        private async void EliminarProducto(Producto producto)
        {
            bool confirmado = await Application.Current!.MainPage!.DisplayAlert(
                "Confirmar eliminación",
                $"¿Está seguro que desea eliminar el producto '{producto.Nombre}'?",
                "Sí", "No");

            if (!confirmado)
                return;

            // Elimina el producto de la lista observable y del repositorio persistente
            Productos.Remove(producto);
            repositorioProductos.Eliminar(producto.Id);

            if (MostrarMensaje != null)
            {
                await MostrarMensaje.Invoke("Producto Eliminado", $"Producto '{producto.Nombre}' eliminado.");
            }
        }
    }
}
