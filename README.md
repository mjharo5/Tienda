# Tienda – Catálogo en línea (Grupo 1)

Problema: Una tienda necesita su catálogo en línea.
Entidades: Categoria (principal) y Producto (dependiente).
Roles: Administrador (gestión) y Cliente (consulta).

**Roles**

| Rol | Tipo | Permisos |
|---|---|---|
| Administrador | Gestión | Crear, editar y eliminar en ambas entidades |
| Cliente | Consulta | Solo ver la lista y el detalle |

## Integrantes
| Turno | Integrante | Fases |
|---|---|---|
| 1 | Maria Jose Haro | Fase 1 y Fase 2 |
| 2 | Diego Baque | Fase 3 |
| 3 | Sheyla Bernal | Fase 4 |
| 4 | Anthony Aguilar  | Fase 5 |

## Aporte de cada integrante
| Integrante | Fase | Aporte | Migración creada |
|---|---|---|---|
| María José Haro | 1 | Proyecto nuevo ASP.NET Core MVC (.NET 10), repositorio en GitHub, entidad Producto con su CRUD por scaffolding y cadena de conexión a `localhost\SQLEXPRESS` | `InitialCreate` |
| María José Haro | 2 | DataAnnotations en Producto (`[Required]`, `[StringLength]`, `[Range]`, `[Display]`) con mensajes en español | `ValidacionesProducto` |
| Diego Baque | 3 | Entidad Categoria con validaciones y CRUD, clave foránea `CategoriaId`, propiedades de navegación y lista desplegable de categorías en los formularios de Producto | `RelacionCategoriaProducto` |
| Sheyla Bernal | 3 (ajuste) | Columna `Descripcion?` en Categoria y corrección del `[Bind]` del controlador | `AgregarDescripcionCategoria` |
| Sheyla Bernal | 4 | Identity con scaffolding, clase `ApplicationUser`, `TiendaContext` heredando de `IdentityDbContext<ApplicationUser>`, `_LoginPartial` en el menú, `[Authorize]` y `[AllowAnonymous]` en `ProductosController`, páginas de registro e inicio de sesión en español y traducción de los errores de Identity | `AgregarIdentity` |
| Anthony Aguilar | 5 | `.AddRoles<IdentityRole>()`, creación de los roles Administrador y Cliente al iniciar con `RoleManager`, y de los dos usuarios de prueba con su rol vía `UserManager` | (sin migración) |
| [Quien lo haga] | Componente autónomo | Restricción por rol en ambos controladores y ocultar los enlaces Crear, Editar y Eliminar según el rol | (sin migración) |

## 4. Usuarios de prueba
Se crean automáticamente al iniciar la aplicación.

| Rol | Correo | Contraseña |
|---|---|---|
| Administrador | admin@tienda.com | Admin123* |
| Cliente | cliente@tienda.com | Cliente123* |

## 5. Cómo ejecutar el proyecto

### Requisitos
- Visual Studio con la carga de trabajo **ASP.NET y desarrollo web** y el SDK de **.NET 10**.
- **SQL Server Express** (instancia `SQLEXPRESS`) y, opcionalmente, **SSMS** para ver las tablas.
- **Git**.

### Pasos
1. **Clonar el repositorio** en Visual Studio (**Git > Clonar repositorio**) o con:
   git clone https://github.com/mjharo5/Tienda.git
   
2. **Abrir la solución** `Tienda.slnx` (o el `.sln`) en Visual Studio.
   
3. **Ajustar la cadena de conexión** en `appsettings.json` al servidor de tu equipo. Por defecto:
   json
   "ConnectionStrings": {
     "TiendaContext": "Server=localhost\\SQLEXPRESS;Database=TiendaDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"}
   Si tu instancia se llama distinto, cambia `localhost\\SQLEXPRESS` por el nombre que usas en SSMS (por ejemplo `.\\SQLEXPRESS` o `(localdb)\\MSSQLLocalDB`). En el JSON la barra se escribe doble.
   
4. **Aplicar las migraciones** en **Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes**, con el proyecto predeterminado `Tienda`:
   Update-Database
   Debe terminar con `Done.` y crear la base `TiendaDb` con sus tablas.
   
5. **Ejecutar** la aplicación con **F5**. Los roles y los usuarios de prueba se crean solos la primera vez.
   
6. **Probar:**
   - Sin sesión: se pueden ver `/Productos` y `/Categorias` y el detalle, sin enlaces de crear, editar ni eliminar.
   - Iniciar sesión como **admin@tienda.com** para crear, editar y eliminar.
   - Iniciar sesión como **cliente@tienda.com**: solo puede ver. Si escribe `/Productos/Create` en el navegador, se le niega el acceso
