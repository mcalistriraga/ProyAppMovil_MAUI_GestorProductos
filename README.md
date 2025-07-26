INFORME ACTUALIZADO DE ARQUITECTURA Y FUNCIONAMIENTO DEL SISTEMA
=================================================================

1. OBJETIVO DEL SISTEMA
-----------------------
El sistema desarrollado es una aplicación multiplataforma (Windows, Android, iOS) creada con .NET MAUI, orientada a la gestión de productos y categorías. Permite su visualización, edición y administración desde una interfaz moderna, responsiva y portable.

2. ARQUITECTURA GENERAL
-----------------------
- Sigue el patrón MVVM (Model-View-ViewModel) para mantener una separación clara entre la lógica de negocio, la interfaz y los datos.
- Utiliza archivos JSON locales (`productos.json`, `categorias.json`) como mecanismo de persistencia.
- Se implementan repositorios para el manejo estructurado de lectura y escritura de datos.
- Las interfaces están desarrolladas en XAML, desacopladas del código lógico (C#).
- Las rutas de acceso a los datos se ajustan automáticamente a la plataforma, garantizando portabilidad.
- Se migró el sistema de mensajería interna desde `MessagingCenter` (obsoleto) hacia `CommunityToolkit.Mvvm.Messaging.WeakReferenceMessenger`, mejorando el manejo de mensajes entre componentes con mayor seguridad y mantenibilidad.

3. COMPONENTES PRINCIPALES
--------------------------
- **Modelos**: `Producto`, `Categoria`, `CategoriaNodo`, entre otros, definen la estructura de los datos.
- **Repositorios**: Manejan el acceso a archivos JSON de manera estructurada y controlada.
- **ViewModels**: Controlan la lógica de presentación, enlazan datos a la interfaz, y gestionan comandos.
- **Vistas (Views)**: Desarrolladas en XAML. Incluyen:
  - FrameDashboard.xaml (menú principal)
  - GestionDeProductos.xaml
  - GestionDeCategorias.xaml
  - DetallesDelProducto.xaml
  - EditarProducto.xaml
  - AgregarCategoria.xaml
  - AgregarSubcategoria.xaml
- **Componentes reutilizables**:
  - EncabezadoEmpresa.xaml: encabezado común con logo, lema y título dinámico.
  - CategoriaItemView.xaml: vista recursiva de categorías con soporte de anidamiento jerárquico.
- **Mensajería interna**:
  - Implementada con `CommunityToolkit.Mvvm.Messaging.WeakReferenceMessenger`.
  - Mensajes específicos definidos en la carpeta `Messages`, como `CategoriaEditadaMessage` y `RecargarCategoriasMessage`.
  - Sustituyó el uso anterior de `MessagingCenter` para comunicar eventos entre ViewModels y Views.

4. FUNCIONALIDAD IMPLEMENTADA
-----------------------------
- Visualización jerárquica de categorías hasta 4 o más niveles.
- Visualización de productos con propiedades generales y específicas por categoría.
- Edición y eliminación de productos.
- Agregado de categorías raíz y subcategorías.
- Truncamiento automático de texto en nombres largos con alerta informativa al pulsar.
- Navegación fluida entre vistas con encabezado coherente y estilo uniforme.
- Comunicación entre componentes mediante mensajes seguros y desacoplados usando `WeakReferenceMessenger`.

5. FLUJO DE FUNCIONAMIENTO
--------------------------
a) Al iniciar, los archivos `productos.json` y `categorias.json` son copiados desde `Resources` a la carpeta local del dispositivo si no existen.  
b) Los ViewModels cargan estos datos mediante los repositorios.  
c) La interfaz se enlaza a los ViewModels mediante binding.  
d) Las acciones del usuario (agregar, editar, eliminar) actualizan los datos y se guardan en JSON local.  
e) La comunicación interna entre vistas y viewmodels usa mensajes definidos con `WeakReferenceMessenger` para mayor robustez y evitar problemas de fugas de memoria.  
f) El diseño visual sigue lineamientos del diseño exportado desde Figma, con botones personalizados y jerarquía clara.

6. BUENAS PRÁCTICAS Y PORTABILIDAD
----------------------------------
- Uso de rutas relativas y copia inicial automática de archivos asegura funcionamiento en Windows, Android e iOS.
- Arquitectura desacoplada y modular, ideal para mantenimiento y escalabilidad.
- Posibilidad de sustituir los archivos JSON por bases de datos locales (SQLite) o sincronización en la nube (Firebase, Azure, etc.) sin afectar la lógica central.
- Uso de la librería oficial `CommunityToolkit.Mvvm` para MVVM, incluida la mensajería, para mantener la aplicación actualizada y segura.
- Git y GitHub utilizados con ramas organizadas (`main`, `releaseX`) y versiones etiquetadas (`vX.Y.Z`).

7. PLANES FUTUROS Y MEJORAS PENDIENTES
--------------------------------------
- Finalizar validaciones al editar/agregar productos.
- Permitir edición y eliminación de categorías y subcategorías.
- Mejorar feedback visual al usuario (diálogos, mensajes, errores).
- Implementar persistencia en la nube o sincronización remota.
- Adaptar para múltiples idiomas e internacionalización.
- Integrar autenticación y roles de usuario si se requiere.

-----------------------------------------------------------
Este informe representa el estado actualizado del sistema  
a la fecha: 25/07/2025
