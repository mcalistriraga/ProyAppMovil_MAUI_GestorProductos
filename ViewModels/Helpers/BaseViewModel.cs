using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace MauiAppGestorMovil.ViewModels.Helpers
{
    /// <summary>
    /// Clase base para todos los ViewModels en el patrón MVVM.
    /// Implementa la interfaz INotifyPropertyChanged, lo que permite
    /// notificar a la interfaz de usuario cuando una propiedad cambia.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Evento que se dispara cuando una propiedad cambia su valor.
        /// Es utilizado por la interfaz gráfica para actualizarse automáticamente.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Método que dispara el evento PropertyChanged para notificar a la UI
        /// que una propiedad ha cambiado de valor.
        /// </summary>
        /// <param name="propertyName">
        /// Nombre de la propiedad que ha cambiado. Se establece automáticamente gracias al atributo CallerMemberName.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            // Dispara el evento, si hay suscriptores
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Método auxiliar para establecer el valor de un campo y notificar el cambio solo si el valor ha cambiado.
        /// Evita llamadas innecesarias a OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T">Tipo del valor de la propiedad.</typeparam>
        /// <param name="backingField">Referencia al campo privado (campo de respaldo).</param>
        /// <param name="value">Nuevo valor a establecer.</param>
        /// <param name="propertyName">Nombre de la propiedad (se establece automáticamente).</param>
        /// <returns>
        /// true si el valor cambió y se notificó a la UI, false si no hubo cambios.
        /// </returns>
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            // Compara si el valor nuevo es igual al actual, si es así no se hace nada
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            // Cambia el valor y notifica a la UI
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
