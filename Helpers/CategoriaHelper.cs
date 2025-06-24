using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using System.Collections.Generic;

namespace MauiAppGestorMovil.Helpers
{
    public static class CategoriaHelper
    {
        public static string ObtenerRutaCategoriaCompleta(int idCategoria, RepositorioCategorias repoCategorias)
        {
            List<string> nombres = new List<string>();
            var categoria = repoCategorias.BuscarPorId(idCategoria);

            while (categoria != null)
            {
                nombres.Insert(0, categoria.Nombre);
                if (categoria.IdPadre == null) break;
                categoria = repoCategorias.BuscarPorId(categoria.IdPadre.Value);
            }

            return string.Join(" > ", nombres);
        }
    }
}
