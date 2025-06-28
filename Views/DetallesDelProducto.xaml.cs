using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.Helpers;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace MauiAppGestorMovil.Views
{
    public partial class DetallesDelProducto : ContentPage
    {
        public DetallesDelProducto(Producto producto)
        {
            InitializeComponent();

            var repoCategorias = new RepositorioCategorias();
            var rutaCategoria = CategoriaHelper.ObtenerRutaCategoriaCompleta(producto.IdCategoria, repoCategorias);
            var propiedadesFormateadas = FormatearPropiedades(producto.PropiedadesEspecificas);

            // ViewModel anónimo con todos los campos necesarios
            var vm = new
            {
                producto.Id,
                producto.Nombre,
                producto.Descripcion,
                producto.Precio,
                producto.Stock,
                producto.IdCategoria,
                CategoriaNombreCompleta = rutaCategoria,
                PropiedadesFormateadas = propiedadesFormateadas
            };

            BindingContext = vm;
        }

        /// <summary>
        /// Convierte el diccionario de propiedades específicas en una cadena tipo: "Marca: Dell, Color: Negro"
        /// </summary>
        private static string FormatearPropiedades(Dictionary<string, string> propiedades)
        {
            if (propiedades == null || propiedades.Count == 0)
                return "N/A";

            List<string> partes = new();
            foreach (var kvp in propiedades)
            {
                partes.Add($"{kvp.Key}: {kvp.Value}");
            }

            return string.Join(", ", partes);
        }

        private async void BtnCerrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
