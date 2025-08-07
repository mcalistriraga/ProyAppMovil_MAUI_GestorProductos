using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.ViewModels.Helpers;

namespace MauiAppGestorMovil.ViewModels
{
    public class CategoriaNodo : BaseViewModel
    {
        public Categoria Categoria { get; set; }

        public CategoriaNodo(Categoria categoria)
        {
            Categoria = categoria;
        }

        public ObservableCollection<CategoriaNodo> Subcategorias { get; set; } = new();

        private bool _estaExpandida;
        public bool EstaExpandida
        {
            get => _estaExpandida;
            set => SetProperty(ref _estaExpandida, value);
        }

        public ICommand ToggleExpandCommand => new Command(() =>
        {
            EstaExpandida = !EstaExpandida;
        });

        // Constante para definir longitud máxima visible del nombre
        public const int MaxNombreVisible = 20;

        // Propiedad que devuelve el nombre truncado si excede el límite
        public string NombreTruncado
        {
            get
            {
                var nombre = Categoria?.Nombre ?? string.Empty;
                return nombre.Length <= MaxNombreVisible
                    ? nombre
                    : nombre.Substring(0, MaxNombreVisible) + "...";
            }
        }

        // Comando que muestra un DisplayAlert si el nombre fue truncado
        public ICommand MostrarNombreCompletoCommand => new Command(async () =>
        {
            var nombre = Categoria?.Nombre;
            if (!string.IsNullOrEmpty(nombre) && nombre.Length > MaxNombreVisible)
            {
                var mainPage = Application.Current?.MainPage;
                if (mainPage != null)
                    await mainPage.DisplayAlert("Nombre completo", nombre, "Cerrar");
                // else: puedes manejar el caso si MainPage es null (log o fallback)
            }
        });

        // ✅ Nueva propiedad que indica si esta categoría es una hoja
        public bool EsHoja => Subcategorias == null || Subcategorias.Count == 0;
    }
}
