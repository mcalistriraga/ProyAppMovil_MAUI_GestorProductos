using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiAppGestorMovil.ViewModels.Helpers;

namespace MauiAppGestorMovil.Models
{
    public class CategoriaNodo : BaseViewModel // 🔁 Hereda de BaseViewModel para notificación de cambios
    {
        public Categoria Categoria { get; set; }
        public ObservableCollection<CategoriaNodo> Subcategorias { get; set; }

        private bool _estaExpandida = false;
        public bool EstaExpandida
        {
            get => _estaExpandida;
            set => SetProperty(ref _estaExpandida, value);
        }

        public ICommand ToggleExpandCommand => new Command(() => EstaExpandida = !EstaExpandida);

        public CategoriaNodo(Categoria categoria)
        {
            Categoria = categoria;
            Subcategorias = new ObservableCollection<CategoriaNodo>();
        }

        public CategoriaNodo()
        {
            Categoria = new Categoria();
            Subcategorias = new ObservableCollection<CategoriaNodo>();
        }
    }
}
