using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiAppGestorMovil.Helpers
{
    /// <summary>
    /// Clase estática que centraliza toda la navegación y alertas en la app.
    /// Permite a los ViewModels interactuar con la UI sin depender directamente de MainPage.
    /// </summary>
    public static class AppNavigator
    {
        /// <summary>
        /// Obtiene la pila de navegación actual.
        /// </summary>
        public static IReadOnlyList<Page> NavigationStack =>
            Application.Current?.MainPage?.Navigation?.NavigationStack ?? new List<Page>();

        /// <summary>
        /// Navega hacia una nueva página de forma asíncrona.
        /// </summary>
        public static async Task PushAsync(Page page)
        {
            var mainPage = Application.Current?.MainPage;

            if (mainPage is NavigationPage navPage)
                await navPage.PushAsync(page);
            else if (mainPage?.Navigation != null)
                await mainPage.Navigation.PushAsync(page);
        }

        /// <summary>
        /// Retorna a la página anterior de forma asíncrona.
        /// </summary>
        public static async Task PopAsync()
        {
            var mainPage = Application.Current?.MainPage;

            if (mainPage is NavigationPage navPage)
                await navPage.PopAsync();
            else if (mainPage?.Navigation != null)
                await mainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Cambia la página principal de la aplicación.
        /// </summary>
        public static void SetMainPage(Page page)
        {
            Application.Current!.MainPage = new NavigationPage(page);
        }

        /// <summary>
        /// Elimina una página específica de la pila de navegación.
        /// </summary>
        public static void RemovePage(Page page)
        {
            Application.Current?.MainPage?.Navigation?.RemovePage(page);
        }

        /// <summary>
        /// Muestra un DisplayAlert simple (solo botón OK).
        /// </summary>
        public static async Task DisplayAlert(string title, string message)
        {
            var mainPage = Application.Current?.MainPage;
            if (mainPage != null)
                await mainPage.DisplayAlert(title, message, "OK");
        }

        /// <summary>
        /// Muestra un DisplayAlert con opción de aceptar/cancelar y retorna el resultado.
        /// </summary>
        public static async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            var mainPage = Application.Current?.MainPage;
            if (mainPage != null)
                return await mainPage.DisplayAlert(title, message, accept, cancel);
            return false;
        }
    }
}
