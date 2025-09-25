
# Plan Organizado para Escalar la App Móvil (MAUI Gestor de Productos)

Este documento describe la estrategia para evolucionar la aplicación móvil desde un prototipo local hasta una solución en la nube escalable.

---

## 1. Versión Base (Persistencia en JSON)
- Nombre: `ProyAppMovil_MAUI_GestorProductos_JSON`
- Estado: Estable, **no se modificará más**.
- Función: Base de referencia y software reusable para otros proyectos.
- Características principales:
  - Persistencia local con archivos JSON.
  - Categorías jerárquicas.
  - Productos asociados a categorías.
  - Interfaz con encabezado y controles estandarizados.

---

## 2. Escalamiento a SQLite (Persistencia Local Avanzada)
- Nombre: `ProyAppMovil_MAUI_GestorProductos_SQLite`
- Propósito: Proveer persistencia robusta en dispositivos sin depender de archivos JSON.
- Ventajas:
  - Consultas más rápidas y estructuradas.
  - Soporte a relaciones y validaciones de datos.
  - Preparación para migrar a entornos cliente-servidor.
- Estrategia:
  - Clonar versión base (JSON).
  - Implementar repositorios con SQLite.
  - Mantener lógica de categorías y productos.
  - Validar migración de datos JSON → SQLite.

---

## 3. Escalamiento a API + MySQL en la Nube
- Nombre: `ProyAppMovil_MAUI_GestorProductos_API_MySQL`
- Propósito: Evolucionar a arquitectura cliente-servidor conectada a un backend.
- Backend: .NET + Entity Framework + Swagger.
- Base de Datos: MySQL.
- Infraestructura Cloud (a elegir, sin costo inicial):
  - **Azure (Free Tier)**
  - **Firebase (Realtime DB o Firestore)**
  - **AWS (Free Tier con RDS MySQL)**
- Estrategia:
  - Clonar versión SQLite.
  - Desarrollar API REST con endpoints CRUD.
  - Integrar autenticación básica (JWT si aplica).
  - Consumir API desde la app MAUI.
  - Subir a nube seleccionada y documentar despliegue.

---

## 4. Lineamiento de Mantenimiento
- Únicamente la versión **API + MySQL en la nube** tendrá soporte a futuro.
- La versión SQLite quedará como referencia y respaldo local.
- La versión JSON será la base histórica y software reusable.

---

## 5. Control de Versiones (GitHub)
- Repositorios separados para cada versión.
- Ramas y tags descriptivos para cada release.
- Convenciones sugeridas:
  - `releaseX-funcionalidad`
  - Tags: `vX.Y.Z-descripcion`
- Mostrar en GitHub solo las versiones relevantes (SQLite y API en la nube).
- Mantener documentación clara en la carpeta `/docs`.

---

## 6. Próximos Pasos
1. Terminar documentación de la versión JSON (base).
2. Clonar proyecto para comenzar `ProyAppMovil_MAUI_GestorProductos_SQLite`.
3. Implementar SQLite con repositorios.
4. Migrar a API REST con MySQL en la nube.
5. Documentar despliegue y publicar repositorios relevantes en GitHub.
