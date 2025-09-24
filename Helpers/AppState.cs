using MauiAppGestorMovil.Repositories;

namespace MauiAppGestorMovil.Helpers
{
    public static class AppState
    {
        // Instancias compartidas de repositorios
        public static RepositorioProductos ProductosRepo { get; } = new RepositorioProductos();
        public static RepositorioCategorias CategoriasRepo { get; } = new RepositorioCategorias();
    }
}
