# Backend iPhone Store API

API REST para una tienda de iPhones con autenticación JWT y manejo de roles.

## Características

- Autenticación JWT con roles (Admin, Seller, User)
- CRUD de productos con variaciones por color
- CRUD de usuarios
- Paginación y búsqueda
- CORS configurado para Angular
- Base de datos SQL Server

## Roles y Permisos

### Admin
- Crear, modificar, eliminar y ver productos
- Crear, modificar, eliminar y ver usuarios

### Seller
- Crear, modificar y ver productos
- Ver usuarios

### User
- Solo ver productos y usuarios

## Endpoints

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario

### Usuarios
- `GET /api/users` - Listar usuarios (con paginación y búsqueda)
- `GET /api/users/{id}` - Obtener usuario por ID
- `PUT /api/users/{id}` - Actualizar usuario (Solo Admin)
- `DELETE /api/users/{id}` - Eliminar usuario (Solo Admin)

### Productos
- `GET /api/products` - Listar productos (con paginación y búsqueda)
- `GET /api/products/{id}` - Obtener producto por ID
- `POST /api/products` - Crear producto (Admin/Seller)
- `PUT /api/products/{id}` - Actualizar producto (Admin/Seller)
- `DELETE /api/products/{id}` - Eliminar producto (Solo Admin)

## Configuración

1. Asegúrate de tener SQL Server ejecutándose
2. La cadena de conexión está configurada para `DESKTOP-108OS4T`
3. La base de datos se crea automáticamente al iniciar la aplicación

## Usuarios de Prueba

- **Admin**: username: `admin`, password: `admin123`
- **Seller**: username: `seller`, password: `seller123`
- **User**: username: `user`, password: `user123`

## Ejecutar la Aplicación

```bash
dotnet run
```

La API estará disponible en:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger: https://localhost:5001/swagger

## CORS

Configurado para permitir requests desde `http://localhost:4200` (Angular).