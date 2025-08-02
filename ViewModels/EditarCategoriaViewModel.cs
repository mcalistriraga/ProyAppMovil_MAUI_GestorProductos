using System.Windows.Input;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Services;
using MauiAppGestorMovil.ViewModels.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.ViewModels
{
    public class EditarCategoriaViewModel : BaseViewModel
    {
        public Categoria Categoria { get; set; } = null!;
        public ObservableCollection<string> Propiedades { get; set; } = new();

        public ICommand EliminarPropiedadCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }
        public ICommand AgregarPropiedadPromptCommand { get; }

        private readonly INavigation _navigation;
        private readonly ICategoriaService _categoriaService;

        public EditarCategoriaViewModel(Categoria categoria, ICategoriaService categoriaService, INavigation navigation)
        {
            Categoria = categoria;
            _categoriaService = categoriaService;
            _navigation = navigation;

            // Copiar propiedades de la categoría original (si existen)
            if (Categoria?.Propiedades != null)
            {
                foreach (var propiedad in Categoria.Propiedades)
                    Propiedades.Add(propiedad);
            }

            // Comandos
            EliminarPropiedadCommand = new Command<string>(EliminarPropiedad);
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(async () => await _navigation.PopModalAsync());
            AgregarPropiedadPromptCommand = new Command(async () => await MostrarPromptAgregarPropiedad());
        }

        private void EliminarPropiedad(string propiedad)
        {
            if (!string.IsNullOrWhiteSpace(propiedad) && Propiedades.Contains(propiedad))
                Propiedades.Remove(propiedad);
        }

        private async void Guardar()
        {
            try
            {
                Categoria.Propiedades = new List<string>(Propiedades);
                await _categoriaService.ActualizarCategoriaAsync(Categoria);
                await _navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await Application.Current?.MainPage?.DisplayAlert("Error al Guardar()", ex.Message, "OK");
            }
        }

        private async Task MostrarPromptAgregarPropiedad()
        {
            // Verificación de null para Application.Current y MainPage para evitar CS8602
            if (Application.Current?.MainPage == null)
                return;

            string? resultadoNullable = await Application.Current.MainPage.DisplayPromptAsync(
                "Nueva Propiedad",
                "Ingrese el nombre de la nueva propiedad:",
                "Agregar",
                "Cancelar",
                placeholder: "Ejemplo: Color",
                maxLength: 50,
                keyboard: Keyboard.Text);

            string resultado = resultadoNullable ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var propiedad = resultado.Trim();

                if (!Propiedades.Contains(propiedad))
                    Propiedades.Add(propiedad);
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "La propiedad ya existe.", "OK");
            }
        }
    }
}