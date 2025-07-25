using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.ViewModels
{
    public class EditarCategoriaViewModel : INotifyPropertyChanged
    {
        private readonly Categoria _categoria;
        private readonly RepositorioCategorias _repositorio;
        private readonly INavigation _navigation;

        public event PropertyChangedEventHandler? PropertyChanged;

        public EditarCategoriaViewModel(Categoria categoriaSeleccionada, RepositorioCategorias repositorio, INavigation navigation)
        {
            _categoria = categoriaSeleccionada ?? throw new ArgumentNullException(nameof(categoriaSeleccionada));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            _nombreCategoria = _categoria.Nombre;

            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Cancelar);
        }

        private string _nombreCategoria;
        public string NombreCategoria
        {
            get => _nombreCategoria;
            set
            {
                if (_nombreCategoria != value)
                {
                    _nombreCategoria = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        protected void OnPropertyChanged([CallerMemberName] string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }

        private async void Guardar()
        {
            if (string.IsNullOrWhiteSpace(NombreCategoria))
            {
                // Validación para evitar posible null reference
                var mainPage = Application.Current?.MainPage;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("Error", "El nombre no puede estar vacío", "OK");
                }
                return;
            }

            _categoria.Nombre = NombreCategoria;
            _repositorio.Actualizar(_categoria);

            // ✅ Envía mensaje de actualización antes de cerrar
            MessagingCenter.Send(this, "CategoriaEditada");

            await _navigation.PopModalAsync();  // sigue siendo modal
        }

        private async void Cancelar()
        {
            await _navigation.PopModalAsync();  // sigue siendo modal
        }
    }
}
