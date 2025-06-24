namespace MauiAppGestorMovil.Models
{
    /// <summary>
    /// Representa un producto dentro del inventario de la aplicación.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo del producto.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada del producto.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la categoría a la que pertenece el producto.
        /// Se usa para enlazar con la categoría correspondiente.
        /// </summary>
        public int IdCategoria { get; set; }

        /// <summary>
        /// Nombre de la categoría asociada.
        /// Es un campo calculado o auxiliar para mostrar en UI.
        /// </summary>
        public string CategoriaNombre { get; set; } = string.Empty;

        /// <summary>
        /// Precio unitario del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Cantidad disponible en stock del producto.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Propiedades específicas adicionales que describen atributos particulares del producto,
        /// almacenadas como pares clave-valor.
        /// Ejemplo: "Color" -> "Rojo", "Tamaño" -> "Grande".
        /// </summary>
        public Dictionary<string, string> PropiedadesEspecificas { get; set; } = new();
    }
}
