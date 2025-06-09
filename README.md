INFORME PRELIMINAR DE ARQUITECTURA Y FUNCIONAMIENTO DEL SISTEMA
================================================================

1. OBJETIVO DEL SISTEMA
-----------------------
El sistema desarrollado es una aplicación multiplataforma (Windows, Android, iOS) creada con .NET MAUI, orientada a la gestión de productos y categorías, permitiendo su visualización, modificación y administración desde una interfaz moderna y portable.

2. ARQUITECTURA GENERAL
-----------------------
- El sistema sigue una arquitectura MVVM (Model-View-ViewModel), separando claramente los modelos de datos, la lógica de negocio y la interfaz de usuario.
- Utiliza archivos JSON como mecanismo de persistencia local para productos y categorías.
- Implementa repositorios para el acceso, escritura y lectura estructurada de los datos.
- La interfaz gráfica está desarrollada en XAML, con vistas desacopladas de la lógica.
- El sistema es portable y las rutas de acceso a datos se gestionan dinámicamente para cada plataforma, evitando dependencias de rutas absolutas.

3. COMPONENTES PRINCIPALES
--------------------------
- **Modelos**: Definen las entidades principales (`Producto`, `Categoria`, etc.) con sus propiedades y relaciones.
- **Repositorios**: Encapsulan la lógica de acceso a datos (lectura/escritura de archivos JSON).
- **ViewModels**: Gestionan el estado y la lógica de presentación para cada vista, exponiendo comandos y colecciones observables para el binding.
- **Vistas (Views)**: Definidas en XAML, representan la interfaz del usuario, enlazadas a los ViewModels.
- **Inicialización de datos**: Al iniciar la app, los archivos de datos son copiados desde la carpeta `Resources` a la ruta local de datos del usuario si no existen, garantizando portabilidad y persistencia inicial.

4. FLUJO DE FUNCIONAMIENTO PRINCIPAL
------------------------------------
a) Al arrancar, la aplicación copia (si es necesario) los archivos `productos.json` y `categorias.json` desde `Resources` a la carpeta local del usuario.
b) Los ViewModels cargan los datos desde estos archivos utilizando los repositorios.
c) La interfaz, mediante binding a los ViewModels, muestra los productos y categorías en listas, permitiendo operaciones de alta, baja y edición.
d) Los cambios realizados por el usuario se reflejan en los archivos locales, manteniendo la persistencia entre sesiones.
e) El sistema es capaz de mostrar mensajes informativos y advertencias (por ejemplo, falta de archivos de datos) durante el desarrollo.

5. PORTABILIDAD Y BUENAS PRÁCTICAS
-----------------------------------
- El uso de rutas relativas y la copia automática de los recursos iniciales garantizan el funcionamiento en todas las plataformas soportadas por MAUI.
- La separación clara de responsabilidades (modelo, repositorio, vista, viewmodel) facilita el mantenimiento, pruebas y escalabilidad del sistema.
- La arquitectura permite, en el futuro, reemplazar la persistencia por otra tecnología (base de datos local o remota) con cambios mínimos en la lógica de negocio.

6. PUNTOS PENDIENTES Y MEJORAS FUTURAS
---------------------------------------
- Implementar la funcionalidad completa para agregar, editar y eliminar productos y categorías desde la interfaz.
- Mejorar la validación de datos y manejo de errores.
- Internacionalización y adaptación de la interfaz a distintos idiomas.
- Sincronización de datos con servicios remotos, si se requiere.

----------------------------------------------------------
Este informe es preliminar y representa el estado actual a la fecha: 09/06/2025.
