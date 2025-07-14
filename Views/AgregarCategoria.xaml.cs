using System;
using MauiAppGestorMovil.Helpers;
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Views
{
    public partial class AgregarCategoria : ContentPage
    {
        private readonly RepositorioCategorias _repo;

        public AgregarCategoria(RepositorioCategorias repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        /* ------------ ACEPTAR ------------ */
        private async void Aceptar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Advertencia", "Ingrese un nombre.", "OK");
                return;
            }

            var nueva = new Categoria
            {
                Id = _repo.GenerarNuevoId(),
                Nombre = NombreEntry.Text.Trim(),
                IdPadre = null,
                Propiedades = new()
            };

            _repo.Agregar(nueva);
            await Navigation.PopModalAsync();   // Volver a Gestión
        }

        /* ------------ CANCELAR ------------ */
        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /* ------------ OCULTAR TECLADO ------------ */
        private void OnFondoTocado(object sender, EventArgs e)
        {
            CloseTecladoHelper.Ocultar();
        }
    }
}
