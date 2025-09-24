using MauiAppGestorMovil.Models;

namespace MauiAppGestorMovil.Messages
{
    public class ProductoAgregadoMessage
    {
        public Producto Producto { get; }

        public ProductoAgregadoMessage(Producto producto)
        {
            Producto = producto;
        }
    }
}
