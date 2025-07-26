
# Guía Profesional de Trabajo Colaborativo con Git y GitHub

## 1. Estructura de Ramas Recomendada

- `main`: rama principal con código en producción.
- `develop` (opcional): rama base para integrar funcionalidades antes de llegar a `main`.
- `feature/nombre-funcionalidad`: para nuevas funciones.
- `fix/nombre-fix`: para corrección de errores.
- `hotfix/nombre-hotfix`: correcciones críticas en producción.
- `releaseX`: ramas de preparación para una entrega específica.

## 2. Buenas Prácticas con Commits

- Commits atómicos: un cambio lógico por commit.
- Mensajes claros: en presente y estilo imperativo.
  - Ejemplo: `Agregar validación de nombre en formulario`
- Evita mensajes como "Update" o "Cambios varios".

## 3. Uso de Tags

- Usar tags semánticos: `v1.0.0`, `v1.1.1-hotfix`, `v3.2.0-texto-truncado`
- Sirven para marcar versiones estables, entregas, releases importantes.
- Comando útil: `git tag -a v1.0.0 -m "Primera versión estable"`

## 4. Pull Requests (PR)

- Siempre haz PR hacia `develop` o `main` desde tu rama `feature` o `fix`.
- Asigna revisores.
- Incluye descripción clara de lo que hace el PR.
- Espera aprobación antes de hacer merge (idealmente con revisión de código).

## 5. Resolución de Conflictos

- Haz `git pull --rebase origin develop` (o rama base) antes de subir tus cambios.
- Resuelve conflictos localmente antes de hacer push.
- No subas archivos conflictivos sin resolver.

## 6. Código Limpio y Comentado

- Aplica principios como DRY y KISS.
- Escribe comentarios donde sea necesario, pero no redundantes.
- Usa convenciones de nombre claras.

## 7. Comunicación

- Usa issues de GitHub para registrar bugs, tareas o mejoras.
- Relaciona PRs con issues usando `Fixes #número`, `Closes #número`, etc.
- Comunica avances en el equipo mediante mensajes o comentarios en GitHub.

## 8. CI/CD y Automatización (opcional pero recomendado)

- Configura GitHub Actions o pipelines si el proyecto lo permite.
- Ejemplos: pruebas automáticas, despliegue, verificación de estilos.

## 9. Backups y Seguridad

- Haz push frecuente a remoto.
- Nunca subas archivos sensibles (.env, claves) sin protección.
- Usa `.gitignore` apropiadamente.

## 10. Herramientas útiles

- `git log --oneline --graph`: historial gráfico.
- `git status`: estado actual.
- `git diff`: diferencias entre versiones.
- `git stash`: guardar cambios temporales.
- GitHub Desktop o extensiones para VSCode pueden facilitar el trabajo visual.
