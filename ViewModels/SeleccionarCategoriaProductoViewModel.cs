using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.ViewModels.Helpers;
using MauiAppGestorMovil.Repositories;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;

namespace MauiAppGestorMovil.ViewModels
{
    /// <summary>
    /// ViewModel para la página de selección de categoría al agregar un producto.
    /// Gestiona la jerarquía de categorías, la selección de hojas válidas
    /// y la navegación hacia la página de agregar producto.
    /// </summary>
    public class SeleccionarCategoriaProductoViewModel : BaseViewModel
    {
        private readonly RepositorioCategorias _repoCategorias;

        /// <summary>
        /// Colección observable de nodos de categorías jerárquicas.
        /// Se enlaza con la CollectionView en XAML.
        /// </summary>
        public ObservableCollection<CategoriaNodo> CategoriasJerarquicas { get; set; } = new();

        private CategoriaNodo? _categoriaNodoSeleccionada;
        /// <summary>
        /// Nodo actualmente seleccionado en la UI.
        /// El setter dispara la validación y posibles alertas.
        /// </summary>
        public CategoriaNodo? CategoriaNodoSeleccionada
        {
            get => _categoriaNodoSeleccionada;
            set
            {
                if (SetProperty(ref _categoriaNodoSeleccionada, value))
                {
                    OnCategoriaSeleccionadaChanged();
                }
            }
        }

        private CategoriaNodo? _categoriaConfirmada;
        /// <summary>
        /// Nodo confirmado tras aceptar el DisplayAlert de confirmación.
        /// </summary>
        public CategoriaNodo? CategoriaConfirmada
        {
            get => _categoriaConfirmada;
            set => SetProperty(ref _categoriaConfirmada, value);
        }

        private bool _categoriaSeleccionadaHojaValida;
        /// <summary>
        /// Indica si la categoría seleccionada es hoja válida con propiedades.
        /// Se enlaza al botón Continuar para habilitarlo/deshabilitarlo.
        /// </summary>
        public bool CategoriaSeleccionadaHojaValida
        {
            get => _categoriaSeleccionadaHojaValida;
            set => SetProperty(ref _categoriaSeleccionadaHojaValida, value);
        }

        /// <summary>
        /// Comando del botón Continuar: navega a AgregarProducto si la hoja es válida.
        /// </summary>
        public ICommand ContinuarCommand { get; }

        /// <summary>
        /// Comando del botón Cancelar: limpia selección y estado.
        /// </summary>
        public ICommand CancelarCommand { get; }

        /// <summary>
        /// Comando expuesto a los controles de categoría para seleccionar un nodo.
        /// Vinculado en XAML a CategoriaItemSeleccionView.
        /// </summary>
        public ICommand CategoriaSeleccionadaCommand { get; }

        /// <summary>
        /// Constructor. Inicializa repositorio, jerarquía y comandos.
        /// </summary>
        public SeleccionarCategoriaProductoViewModel()
        {
            _repoCategorias = new RepositorioCategorias();

            // Construye la jerarquía padre-hijo de categorías
            ConstruirJerarquiaDeCategorias();

            // Comando para continuar a la página AgregarProducto
            ContinuarCommand = new Command(async () =>
            {
                if (CategoriaConfirmada == null || !CategoriaConfirmada.EsHoja)
                    return;

                var categoria = CategoriaConfirmada.Categoria;

                // Validación: categoría sin propiedades
                if (categoria?.Propiedades == null || categoria.Propiedades.Count == 0)
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Categoría sin propiedades",
                            "La categoría seleccionada no tiene propiedades definidas.",
                            "OK");
                    }
                    return;
                }

                // Navega a AgregarProducto pasando la categoría confirmada
                if (Application.Current?.MainPage?.Navigation != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new Views.AgregarProducto(categoria)
                    );
                }
            });

            // Comando para cancelar la selección
            CancelarCommand = new Command(() =>
            {
                CategoriaConfirmada = null;
                CategoriaSeleccionadaHojaValida = false;
                CategoriaNodoSeleccionada = null;
            });

            // Comando usado por el control CategoriaItemSeleccionView para seleccionar nodos
            CategoriaSeleccionadaCommand = new Command<CategoriaNodo>(nodo =>
            {
                CategoriaNodoSeleccionada = nodo;
            });
        }

        /// <summary>
        /// Maneja los cambios de selección de categoría.
        /// Evalúa si es hoja, si tiene propiedades y muestra alertas de confirmación.
        /// </summary>
        private async void OnCategoriaSeleccionadaChanged()
        {
            if (CategoriaNodoSeleccionada == null)
                return;

            var nodo = CategoriaNodoSeleccionada;

            // Caso 1: nodo no es hoja
            if (!nodo.EsHoja)
            {
                CategoriaSeleccionadaHojaValida = false;
                CategoriaNodoSeleccionada = null;
                if (Application.Current?.MainPage != null)
                    await Application.Current.MainPage.DisplayAlert(
                        "Información",
                        "Esta no es una categoría hoja. Debes seleccionar una categoría hoja.",
                        "Cerrar");
                return;
            }

            // Caso 2: hoja sin propiedades asignadas
            var categoria = nodo.Categoria;
            if (categoria?.Propiedades == null || categoria.Propiedades.Count == 0)
            {
                CategoriaSeleccionadaHojaValida = false;
                CategoriaNodoSeleccionada = null;
                if (Application.Current?.MainPage != null)
                    await Application.Current.MainPage.DisplayAlert(
                        "Categoría sin propiedades",
                        $"Debes dirigirte a la sección de gestión de categorías y asignar al menos una propiedad a la categoría \"{categoria?.Nombre}\" para poder agregar un producto.",
                        "Cerrar");
                return;
            }

            // Caso 3: hoja válida → confirmación del usuario
            if (Application.Current?.MainPage != null)
            {
                bool confirmar = await Application.Current.MainPage.DisplayAlert(
                    "Confirmar selección",
                    $"¿Deseas seleccionar la categoría: \"{categoria?.Nombre}\"?",
                    "Aceptar",
                    "Cancelar");

                if (confirmar)
                {
                    CategoriaConfirmada = nodo;
                    CategoriaSeleccionadaHojaValida = true;
                }
                else
                {
                    CategoriaConfirmada = null;
                    CategoriaSeleccionadaHojaValida = false;
                    CategoriaNodoSeleccionada = null;
                }
            }
        }

        /// <summary>
        /// Construye la jerarquía de categorías padre-hijo a partir del repositorio.
        /// Los nodos sin padre se agregan a la colección principal.
        /// </summary>
        private void ConstruirJerarquiaDeCategorias()
        {
            var todas = _repoCategorias.ObtenerTodas();
            var mapaNodos = new Dictionary<int, CategoriaNodo>();

            // Crea nodo para cada categoría
            foreach (var cat in todas)
                mapaNodos[cat.Id] = new CategoriaNodo(cat);

            CategoriasJerarquicas.Clear();

            // Construye jerarquía padre-hijo
            foreach (var nodo in mapaNodos.Values)
            {
                if (nodo.Categoria.IdPadre == null)
                {
                    // Nodo raíz
                    CategoriasJerarquicas.Add(nodo);
                }
                else if (mapaNodos.TryGetValue(nodo.Categoria.IdPadre.Value, out var padre))
                {
                    // Nodo hijo agregado a la colección de subcategorías
                    padre.Subcategorias.Add(nodo);
                }
            }
        }
    }
}
