using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System;
using System.Threading.Tasks;

namespace MauiAppGestorMovil.ViewModels
{
    public class GestionDeProductosViewModel : BindableObject
    {
        public ObservableCollection<Producto> Productos { get; set; }

        public ICommand AgregarProductoCommand { get; }
        public ICommand VerProductoCommand { get; }

        public Func<string, string, Task>? MostrarMensaje { get; set; }

        private int proximoId = 1;

        private RepositorioProductos repositorioProductos;
        private RepositorioCategorias repositorioCategorias;

        public GestionDeProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>();
            AgregarProductoCommand = new Command(async () => await AgregarProducto());
            VerProductoCommand = new Command(VerProducto);

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

        private void VerProducto()
        {
            // Puedes reemplazar esto luego por navegación real
            MostrarMensaje?.Invoke("Ver", "Funcionalidad de 'Ver producto' en desarrollo.");
        }
    }
}
