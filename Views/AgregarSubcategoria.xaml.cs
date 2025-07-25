using MauiAppGestorMovil.Helpers;   // ??  NUEVO
using MauiAppGestorMovil.Models;
using MauiAppGestorMovil.Repositories;
using MauiAppGestorMovil.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace MauiAppGestorMovil.Views
{
    public partial class AgregarSubcategoria : ContentPage
    {
        private readonly RepositorioCategorias _repo;
        private readonly CategoriaNodo _padre;

        public AgregarSubcategoria(RepositorioCategorias repo, CategoriaNodo padre)
        {
            InitializeComponent();
            _repo = repo;
            _padre = padre;
        }

        /*???????????????????????
         *  Ocultar teclado tocando el fondo
         *??????????????????????*/
        private void OnFondoTocado(object sender, EventArgs e) => CloseTecladoHelper.Ocultar();

        /*???????????????????????
         *  CANCELAR
         *??????????????????????*/
        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            CloseTecladoHelper.Ocultar();          // ??  NUEVO
            await Navigation.PopModalAsync();
        }

        /*???????????????????????
         *  ACEPTAR
         *??????????????????????*/
        private async void Aceptar_Clicked(object sender, EventArgs e)
        {
            string nombre = NombreEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                await DisplayAlert("Aviso", "Ingrese un nombre válido.", "OK");
                return;
            }

            var nueva = new Categoria
            {
                Id = _repo.GenerarNuevoId(),
                Nombre = nombre,
                IdPadre = _padre.Categoria.Id,
                Propiedades = new()
            };

            _repo.Agregar(nueva);

            CloseTecladoHelper.Ocultar();          // ??  NUEVO
            await Navigation.PopModalAsync();
        }
    }
}