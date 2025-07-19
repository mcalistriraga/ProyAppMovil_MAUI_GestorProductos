INFORME PRELIMINAR DE ARQUITECTURA Y FUNCIONAMIENTO DEL SISTEMA
================================================================

1. OBJETIVO DEL SISTEMA
-----------------------
El sistema desarrollado es una aplicación multiplataforma (Windows, Android, iOS) creada con .NET MAUI, orientada a la gestión de productos y categorías. Permite su visualización, edición y administración desde una interfaz moderna y portable, con soporte para estructuras jerárquicas y propiedades específicas por categoría.

2. ARQUITECTURA GENERAL
-----------------------
- El sistema sigue una arquitectura MVVM (Model-View-ViewModel), separando claramente los modelos de datos, la lógica de negocio y la interfaz de usuario.
- Utiliza archivos JSON como mecanismo de persistencia local para productos y categorías.
- Implementa repositorios desacoplados que gestionan la lectura y escritura estructurada de los datos.
- La interfaz gráfica está desarrollada en XAML, con vistas modulares y reutilizables (como encabezados, tarjetas, botones).
- Las rutas de acceso a datos se gestionan dinámicamente según la plataforma, garantizando portabilidad y evitando rutas absolutas.

3. COMPONENTES PRINCIPALES
--------------------------
- **Modelos**: Entidades como `Producto` y `Categoria`, incluyendo soporte para propiedades específicas según el tipo de producto.
- **Repositorios**: Encapsulan la lógica de acceso a archivos JSON (`productos.json`, `categorias.json`).
- **ViewModels**: Gestionan el estado de cada vista, incluyendo comandos y colecciones observables para interacción fluida con la interfaz.
- **Vistas (Views)**: En XAML, separadas en componentes reutilizables (encabezado, ítems de producto, ítems de categoría).
- **Inicialización**: Al primer inicio, los datos base son copiados desde `Resources` al almacenamiento local del usuario para garantizar persistencia y portabilidad.

4. FUNCIONAMIENTO ACTUAL DEL SISTEMA
------------------------------------
- Vista de carga inicial con transición a panel de control.
- Pantalla principal con acceso a gestión de productos y gestión de categorías.
- Visualización de productos en tarjetas, con botón para ver detalles.
- Página de detalles del producto mostrando nombre, categoría, descripción, propiedades específicas, cantidad y precio.
- Gestión de categorías con estructura jerárquica hasta 4 niveles, usando `CollectionView` anidado.
- Truncamiento visual de nombres largos con alerta emergente al tocar.
- Funcionalidad para agregar categorías raíz y subcategorías, mediante pantallas completas en lugar de prompts emergentes.
- Funcionalidad para editar y eliminar categorías individuales.
- Confirmaciones para evitar eliminaciones accidentales (sin eliminación en cascada por ahora).
- Datos persistentes en cada sesión.

5. PORTABILIDAD Y BUENAS PRÁCTICAS
-----------------------------------
- Uso de rutas relativas y recursos embebidos garantiza portabilidad entre plataformas MAUI.
- Separación clara de responsabilidades: modelo de datos, lógica de acceso, presentación y vista.
- Estilos visuales personalizados y consistentes mediante componentes reutilizables.
- Arquitectura abierta para permitir futura migración a base de datos o servicios REST sin afectar las vistas.

6. PENDIENTES Y MEJORAS PLANIFICADAS
-------------------------------------
- Eliminar en cascada subcategorías y productos al borrar una categoría padre (protegido por rol en versiones futuras).
- Mejorar validación de formularios y mensajes de error amigables.
- Agregar control de acceso y roles de usuario (por ejemplo, superusuario).
- Internacionalización y soporte multilenguaje.
- Sincronización remota de datos (Firebase, Azure u otro backend).
- Migración opcional a persistencia con base de datos local (SQLite) o servicio en la nube.

----------------------------------------------------------
Este informe es preliminar y representa el estado actual del sistema a la fecha: 19/07/2025.
