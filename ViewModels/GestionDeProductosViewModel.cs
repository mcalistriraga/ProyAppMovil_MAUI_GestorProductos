using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System.Linq;
using System;

namespace MauiAppGestorMovil.ViewModels
{
    public class GestionDeProductosViewModel : BindableObject
    {
        private RepositorioProductos _repoProductos;
        private RepositorioCategorias _repoCategorias;

        public ObservableCollection<ProductoVisual> Productos { get; }

        public ICommand AgregarProductoCommand { get; }

        // ✅ Agrega esta propiedad
        public Action<string, string>? MostrarMensaje { get; set; }

        public GestionDeProductosViewModel()
        {
            _repoProductos = new RepositorioProductos();
            _repoCategorias = new RepositorioCategorias();

            Productos = new ObservableCollection<ProductoVisual>(
                _repoProductos.ObtenerTodos()
                    .Select(p => new ProductoVisual
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        CategoriaNombre = _repoCategorias.BuscarPorId(p.IdCategoria)?.Nombre ?? "(Sin categoría)"
                    }));

            AgregarProductoCommand = new Command(AgregarProducto);
        }

        private void AgregarProducto()
        {
            // ✅ Usa la propiedad para mostrar un mensaje desde la vista
            MostrarMensaje?.Invoke("Agregar", "Funcionalidad en construcción");
        }
    }


    public class ProductoVisual
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; }
        public string CategoriaNombre { get; set; } = "";
    }
}

