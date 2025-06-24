namespace MauiAppGestorMovil.Models
{
    /// <summary>
    /// Representa una categoría dentro del sistema de inventario.
    /// Puede ser una categoría raíz o una subcategoría.
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo de la categoría.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la categoría padre.
        /// Será null si esta categoría es raíz (no tiene padre).
        /// </summary>
        public int? IdPadre { get; set; }  // null si es una categoría raíz

        /// <summary>
        /// Lista de propiedades específicas que aplican a esta categoría.
        /// Ejemplo: ["Marca", "Garantía", "Color"]
        /// </summary>
        public List<string> Propiedades { get; set; } = new();
    }
}
