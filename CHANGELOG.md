# CHANGELOG - ProyAppMovil\_MAUI\_GestorProductos

## \[v1.1.0-eliminacion-estable] - 2025-06-24

### 🚀 Mejoras

* Se migró de `ListView` a `CollectionView` en la vista `GestionDeProductos.xaml` para una visualización más moderna y estable.
* Mejora notable en la estabilidad al eliminar productos de la lista, evitando cierres inesperados (abortos de la app).
* El manejo de la colección observable `Productos` se realiza ahora en el hilo principal (`MainThread.BeginInvokeOnMainThread`) para evitar conflictos de UI thread.

### ✅ Validaciones realizadas

* Se eliminaron productos desde el inicio, el medio y el final sin errores.
* Pruebas exitosas al dejar la lista vacía y volver a poblarla.

### 📁 Archivos modificados

* `Views/GestionDeProductos.xaml`: Se reemplazó `ListView` por `CollectionView`.
* `ViewModels/GestionDeProductosViewModel.cs`: Se actualizó el método `EliminarProducto` para garantizar el manejo en el hilo principal.

### 📌 Observaciones

* Estos cambios están registrados en la rama `release2`.
* Se recomienda mantener `CollectionView` como componente principal para futuras listas.

---

**Tag asociado:** `v1.1.0-eliminacion-estable`
