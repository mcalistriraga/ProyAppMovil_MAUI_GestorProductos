using MauiAppGestorMovil.Models;

namespace MauiAppGestorMovil.Services
{
    public interface ICategoriaService
    {
        Task ActualizarCategoriaAsync(Categoria categoria);
        // Aquí puedes agregar más métodos en el futuro (como ObtenerPorId, Eliminar, etc.)
    }
}

