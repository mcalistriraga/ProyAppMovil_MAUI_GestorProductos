using System.Collections.ObjectModel;

namespace MauiAppGestorMovil.Models
{
    public class CategoriaNodo
    {
        public Categoria Categoria { get; set; }
        public ObservableCollection<CategoriaNodo> Subcategorias { get; set; }

        public CategoriaNodo(Categoria categoria)
        {
            Categoria = categoria;
            Subcategorias = new ObservableCollection<CategoriaNodo>();
        }

        // Constructor vacío opcional por si se usa sin parámetro
        public CategoriaNodo()
        {
            Categoria = new Categoria(); // Inicializa con objeto vacío para evitar null
            Subcategorias = new ObservableCollection<CategoriaNodo>();
        }
    }
}
