using MauiAppGestorMovil.Helpers;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace MauiAppGestorMovil.Views
{
    public partial class EditarProducto : ContentPage
    {
        private readonly Producto productoOriginal;
        private readonly Dictionary<string, Entry> entradasPropiedades = new(); // Para acceder luego a los valores
        private readonly RepositorioCategorias repoCategorias;

        public EditarProducto(Producto producto)
        {
            InitializeComponent();
            productoOriginal = producto;
            repoCategorias = new RepositorioCategorias();

            // Cargar datos base
            entryNombre.Text = producto.Nombre;
            editorDescripcion.Text = producto.Descripcion;
            entryCantidad.Text = producto.Stock.ToString();
            entryPrecio.Text = producto.Precio.ToString("F2");

            // Mostrar la ruta de categoría (solo visual)
            var ruta = CategoriaHelper.ObtenerRutaCategoriaCompleta(producto.IdCategoria, repoCategorias);
            labelRutaCategoria.Text = ruta;

            // Cargar propiedades específicas dinámicamente
            CargarPropiedadesEspecificas();
        }

        private void CargarPropiedadesEspecificas()
        {
            stackPropiedades.Children.Clear();
            entradasPropiedades.Clear();

            var categoria = repoCategorias.BuscarPorId(productoOriginal.IdCategoria);
            if (categoria == null || categoria.Propiedades.Count == 0)
            {
                stackPropiedades.Children.Add(new Label
                {
                    Text = "Sin propiedades específicas definidas para esta categoría.",
                    TextColor = Colors.Gray,
                    FontAttributes = FontAttributes.Italic
                });
                return;
            }

            foreach (var nombrePropiedad in categoria.Propiedades)
            {
                if (!productoOriginal.PropiedadesEspecificas.TryGetValue(nombrePropiedad, out string? valorExistente) || string.IsNullOrEmpty(valorExistente))
                {
                    valorExistente = "N/A";
                }

                stackPropiedades.Children.Add(new Label
                {
                    Text = nombrePropiedad + ":",
                    TextColor = Colors.Black
                });

                var entry = new Entry
                {
                    Text = valorExistente,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black
                };

                stackPropiedades.Children.Add(entry);
                entradasPropiedades[nombrePropiedad] = entry;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryNombre.Text) ||
                string.IsNullOrWhiteSpace(entryCantidad.Text) ||
                string.IsNullOrWhiteSpace(entryPrecio.Text))
            {
                await DisplayAlert("Advertencia", "Por favor complete todos los campos requeridos.", "OK");
                return;
            }

            productoOriginal.Nombre = entryNombre.Text;
            productoOriginal.Descripcion = editorDescripcion.Text;
            productoOriginal.Stock = int.TryParse(entryCantidad.Text, out int cantidad) ? cantidad : 0;
            productoOriginal.Precio = decimal.TryParse(entryPrecio.Text, out decimal precio) ? precio : 0;

            Dictionary<string, string> nuevasPropiedades = new();
            foreach (var kvp in entradasPropiedades)
            {
                string valor = string.IsNullOrWhiteSpace(kvp.Value.Text) ? "N/A" : kvp.Value.Text!;
                nuevasPropiedades[kvp.Key] = valor;
            }

            productoOriginal.PropiedadesEspecificas = nuevasPropiedades;

            await Navigation.PopAsync();
        }

        private async void OnCerrarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnFondoTocado(object sender, EventArgs e)
        {
            CloseTecladoHelper.Ocultar();
        }
    }
}
