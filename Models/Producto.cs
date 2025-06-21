namespace MauiAppGestorMovil.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdCategoria { get; set; }

        // Models/Producto.cs
        public string CategoriaNombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public int Stock { get; set; }

        // Otros atributos que necesites...
    }
}
    