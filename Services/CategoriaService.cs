using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System.Threading.Tasks;

namespace MauiAppGestorMovil.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly RepositorioCategorias _repoCategorias = new();

        public Task ActualizarCategoriaAsync(Categoria categoria)
        {
            _repoCategorias.Actualizar(categoria);
            return Task.CompletedTask;
        }
    }
}