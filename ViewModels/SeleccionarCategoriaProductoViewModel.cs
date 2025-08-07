using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.ViewModels.Helpers;
using MauiAppGestorMovil.Repositories;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MauiAppGestorMovil.ViewModels
{
    public class SeleccionarCategoriaProductoViewModel : BaseViewModel
    {
        private readonly RepositorioCategorias _repoCategorias;

        public ObservableCollection<CategoriaNodo> CategoriasJerarquicas { get; set; } = new();

        private CategoriaNodo? _categoriaNodoSeleccionada;
        public CategoriaNodo? CategoriaNodoSeleccionada
        {
            get => _categoriaNodoSeleccionada;
            set
            {
                if (SetProperty(ref _categoriaNodoSeleccionada, value))
                {
                    bool esHojaValida = false;

                    if (value is not null)
                    {
                        var categoria = value.Categoria;
                        esHojaValida = value.EsHoja &&
                            categoria?.Propiedades != null &&
                            categoria.Propiedades.Count > 0;
                    }

                    CategoriaSeleccionadaHojaValida = esHojaValida;
                    OnPropertyChanged(nameof(CategoriaSeleccionadaHojaValida));
                }
            }
        }

        private bool _categoriaSeleccionadaHojaValida;
        public bool CategoriaSeleccionadaHojaValida
        {
            get => _categoriaSeleccionadaHojaValida;
            set => SetProperty(ref _categoriaSeleccionadaHojaValida, value);
        }

        public ICommand ContinuarCommand { get; }

        public SeleccionarCategoriaProductoViewModel()
        {
            _repoCategorias = new RepositorioCategorias();
            ConstruirJerarquiaDeCategorias();

            ContinuarCommand = new Command(async () =>
            {
                var categoriaNodo = CategoriaNodoSeleccionada;

                if (categoriaNodo is null)
                    return;

                if (!categoriaNodo.EsHoja)
                    return;

                var categoria = categoriaNodo.Categoria;

                if (categoria == null || categoria.Propiedades == null || categoria.Propiedades.Count == 0)
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Categoría sin propiedades",
                            "La categoría seleccionada no tiene propiedades definidas. Por favor, diríjase a la sección de Gestión de Categorías y edítela o seleccione otra categoría.",
                            "OK");
                    }
                    return;
                }

                if (Application.Current?.MainPage?.Navigation != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new Views.AgregarProducto(categoria)
                    );
                }
            });
        }

        private void ConstruirJerarquiaDeCategorias()
        {
            var todas = _repoCategorias.ObtenerTodas();
            var mapaNodos = new Dictionary<int, CategoriaNodo>();

            foreach (var cat in todas)
            {
                mapaNodos[cat.Id] = new CategoriaNodo(cat);
            }

            CategoriasJerarquicas.Clear();

            foreach (var nodo in mapaNodos.Values)
            {
                if (nodo.Categoria.IdPadre == null)
                {
                    CategoriasJerarquicas.Add(nodo);
                }
                else if (mapaNodos.TryGetValue(nodo.Categoria.IdPadre.Value, out var padre))
                {
                    padre.Subcategorias.Add(nodo);
                }
            }
        }
    }
}
