# Sistema de Gestión de Tienda de Celulares

Aplicación web desarrollada con .NET 9 (Backend) y Angular 19 (Frontend) para la gestión completa de una tienda de celulares con sistema de roles y variantes de productos por color.

## Características Principales

### Funcionalidades Implementadas
- **Gestión de Celulares**: CRUD completo con variantes por color y precio
- **Gestión de Usuarios**: Administración completa de usuarios con roles
- **Autenticación JWT**: Sistema de autenticación seguro con tokens
- **Sistema de Roles**: Permisos diferenciados por tipo de usuario
- **Carrito de Compras**: Funcionalidad completa de carrito
- **Catálogo Dinámico**: Visualización de celulares con filtros y búsqueda
- **Paginación**: Navegación eficiente por grandes volúmenes de datos

### Sistema de Roles
- **Admin**: Crear, Modificar, Eliminar, Ver (celulares y usuarios)
- **Seller**: Crear, Modificar, Ver (solo celulares)
- **User**: Ver (solo celulares) + Carrito de compras

### Tecnologías Utilizadas

#### Backend
- .NET 9 Web API
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- BCrypt para seguridad de contraseñas
- Swagger para documentación API

#### Frontend
- Angular 19 (Standalone Components)
- TypeScript
- Responsive Design
- HTTP Client para comunicación con API

## Estructura del Proyecto

```
SolvexPruebaTecnica/
├── BackendIphoneStore/         # Backend .NET 9
│   ├── Controllers/            # Controladores API
│   │   ├── AuthController.cs
│   │   ├── ProductsController.cs
│   │   ├── UsersController.cs
│   │   └── CartController.cs
│   ├── Models/                 # Modelos de datos
│   │   ├── User.cs
│   │   ├── Product.cs
│   │   ├── ProductVariation.cs
│   │   └── Cart.cs
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Data/                   # DbContext y configuración
│   ├── Services/               # Servicios (JWT)
│   └── Program.cs              # Configuración principal
├── FrontendIphoneStore/        # Frontend Angular 19
│   ├── src/app/
│   │   ├── components/         # Componentes UI
│   │   │   ├── login/
│   │   │   ├── register/
│   │   │   ├── products/
│   │   │   ├── users/
│   │   │   ├── cart/
│   │   │   └── navbar/
│   │   ├── services/           # Servicios HTTP
│   │   ├── models/             # Interfaces TypeScript
│   │   ├── guards/             # Guards de autenticación
│   │   └── interceptors/       # Interceptores HTTP
│   └── package.json
├── .gitignore
├── README.md
└── start-dev.bat              # Script de inicio automático
```

## Instalación y Ejecución

### Prerrequisitos
- .NET 9 SDK
- Node.js (v18 o superior)
- SQL Server LocalDB

### Opción 1: Script Automático
```bash
# Ejecutar desde la raíz del proyecto
start-dev.bat
```

### Opción 2: Manual

#### Backend
1. Navegar al directorio del API:
   ```bash
   cd BackendIphoneStore/BackendIphoneStore
   ```

2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

3. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

   La API estará disponible en: `https://localhost:5001`

#### Frontend
1. Navegar al directorio del frontend:
   ```bash
   cd FrontendIphoneStore
   ```

2. Instalar dependencias:
   ```bash
   npm install
   ```

3. Ejecutar la aplicación:
   ```bash
   npm start
   ```

   La aplicación estará disponible en: `http://localhost:4200`

## Credenciales de Acceso

### Usuario Administrador por Defecto
- **Email**: admin@iphone.com
- **Password**: admin123
- **Rol**: Admin

## Funcionalidades Detalladas

### Gestión de Celulares
- Crear celulares con múltiples variantes de color
- Cada variante puede tener precio diferente
- Ejemplo: iPhone 15 (Negro: $1200, Azul: $1200, Verde: $1300)
- Control de stock por variante
- Búsqueda y filtrado por nombre/descripción
- Paginación de resultados

### Gestión de Usuarios
- Registro de nuevos usuarios con roles
- Visualización en tabla con paginación
- Búsqueda por nombre de usuario o email
- Edición de usuarios (solo Admin)
- Eliminación de usuarios (solo Admin)

### Carrito de Compras
- Agregar productos al carrito
- Selección de variante (color) específica
- Visualización de productos en carrito
- Cálculo automático de totales

### Catálogo de Celulares
- Visualización dinámica por color seleccionado
- Precios actualizados según variante
- Información de stock disponible
- Interfaz responsive para móviles

## API Endpoints

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario

### Productos
- `GET /api/products` - Listar celulares (con paginación y búsqueda)
- `GET /api/products/{id}` - Obtener celular específico
- `POST /api/products` - Crear celular (Admin/Seller)
- `PUT /api/products/{id}` - Actualizar celular (Admin/Seller)
- `DELETE /api/products/{id}` - Eliminar celular (Admin)

### Usuarios
- `GET /api/users` - Listar usuarios (Admin)
- `GET /api/users/{id}` - Obtener usuario específico (Admin)
- `PUT /api/users/{id}` - Actualizar usuario (Admin)
- `DELETE /api/users/{id}` - Eliminar usuario (Admin)

### Carrito
- `GET /api/cart` - Obtener carrito del usuario
- `POST /api/cart` - Agregar producto al carrito
- `DELETE /api/cart/{id}` - Eliminar producto del carrito

## Modelo de Datos

### Celular
```json
{
  "id": 1,
  "name": "iPhone 15",
  "description": "iPhone 15 con chip A17 Pro",
  "imageUrl": "https://ejemplo.com/iphone15.jpg",
  "variations": [
    {
      "id": 1,
      "color": "Negro",
      "price": 1200.00,
      "stock": 25
    },
    {
      "id": 2,
      "color": "Verde",
      "price": 1300.00,
      "stock": 15
    }
  ]
}
```

### Usuario
```json
{
  "id": 1,
  "username": "admin",
  "email": "admin@iphone.com",
  "role": "Admin",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

### Carrito
```json
{
  "id": 1,
  "userId": 1,
  "productVariationId": 1,
  "quantity": 2,
  "addedAt": "2024-01-01T00:00:00Z"
}
```

## Seguridad

- **Autenticación JWT**: Tokens seguros con expiración
- **Autorización por roles**: Endpoints protegidos según permisos
- **Hash de contraseñas**: BCrypt para almacenamiento seguro
- **CORS**: Configurado para desarrollo
- **Guards**: Protección de rutas en Angular
- **Interceptores**: Manejo automático de tokens

## Características Técnicas

### Backend (.NET 9)
- **Arquitectura**: Web API con patrón Repository
- **Base de datos**: Entity Framework Core con SQL Server LocalDB
- **Autenticación**: JWT Bearer tokens
- **Documentación**: Swagger/OpenAPI
- **Validación**: Data Annotations
- **Inyección de dependencias**: Built-in DI container

### Frontend (Angular 19)
- **Arquitectura**: Standalone Components
- **Estado**: Servicios con RxJS
- **Routing**: Angular Router con guards
- **HTTP**: HttpClient con interceptores
- **Formularios**: Reactive Forms
- **Responsive**: CSS Grid y Flexbox

## Patrones de Diseño Implementados

- **Repository Pattern**: Acceso a datos en el backend
- **Dependency Injection**: En ambas aplicaciones
- **Observer Pattern**: RxJS en Angular
- **Guard Pattern**: Protección de rutas
- **Interceptor Pattern**: Manejo de HTTP requests

## URLs de Acceso

- **Frontend**: http://localhost:4200
- **Backend API**: https://localhost:5001
- **Swagger**: https://localhost:5001/swagger

## Desarrollo

Para desarrollo local, ambas aplicaciones deben ejecutarse simultáneamente. El frontend está configurado para comunicarse automáticamente con el backend local.

---

**Desarrollado para prueba técnica - Sistema completo de gestión de tienda de celulares**