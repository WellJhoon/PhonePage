# Sistema de Gestión de Tienda de Celulares

Aplicación web desarrollada con .NET C# (Backend) y Angular (Frontend) para la gestión completa de una tienda de celulares.

## Características Principales

### Funcionalidades Implementadas
- **Gestión de Celulares**: CRUD completo con variantes por color y precio
- **Gestión de Usuarios**: Administración completa de usuarios
- **Autenticación por Roles**: Sistema de permisos diferenciados
- **Catálogo Dinámico**: Visualización de celulares con filtros y búsqueda
- **Paginación**: Navegación eficiente por grandes volúmenes de datos

### Sistema de Roles
- **Admin**: Crear, Modificar, Eliminar, Ver (celulares y usuarios)
- **Seller**: Crear, Modificar, Ver (solo celulares)
- **User**: Ver (solo celulares)

### Tecnologías Utilizadas

#### Backend
- .NET 8 Web API
- Entity Framework Core
- SQL Server LocalDB
- JWT Authentication
- BCrypt para seguridad de contraseñas

#### Frontend
- Angular 15
- Angular Material
- TypeScript
- Responsive Design

## Estructura del Proyecto

```
SolvexPruebaTecnica/
├── ClothingStoreApi/           # Backend .NET
│   ├── Models/                 # Modelos de datos
│   ├── Data/                   # Contexto de base de datos
│   ├── Controllers/            # Controladores API
│   ├── Services/               # Servicios de negocio
│   └── Program.cs              # Configuración principal
├── ClothingStoreFrontend/      # Frontend Angular
│   ├── src/app/
│   │   ├── components/         # Componentes de UI
│   │   ├── services/           # Servicios HTTP
│   │   ├── models/             # Interfaces TypeScript
│   │   └── app.module.ts       # Módulo principal
│   └── package.json
└── README.md
```

## Instalación y Ejecución

### Prerrequisitos
- .NET 8 SDK
- Node.js (v14 o superior)
- SQL Server LocalDB

### Backend

1. Navegar al directorio del API:
   ```bash
   cd ClothingStoreApi/ClothingStoreApi
   ```

2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

3. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

   La API estará disponible en: `https://localhost:7000`

### Frontend

1. Navegar al directorio del frontend:
   ```bash
   cd ClothingStoreFrontend
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
- **Email**: admin@clothing.com
- **Password**: admin123
- **Rol**: Administrador

## Funcionalidades Detalladas

### Gestión de Celulares
- Crear celulares con múltiples variantes de color
- Cada variante puede tener precio diferente
- Subida de imágenes mediante URL
- Control de stock por variante
- Búsqueda y filtrado
- Paginación de resultados

### Gestión de Usuarios
- Registro de nuevos usuarios
- Asignación de roles
- Visualización en tabla con paginación
- Búsqueda por nombre o email
- Eliminación de usuarios (solo Admin)

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
- `GET /api/products` - Listar celulares
- `GET /api/products/{id}` - Obtener celular
- `POST /api/products` - Crear celular
- `PUT /api/products/{id}` - Actualizar celular
- `DELETE /api/products/{id}` - Eliminar celular

### Usuarios
- `GET /api/users` - Listar usuarios (Admin)
- `GET /api/users/{id}` - Obtener usuario (Admin)
- `PUT /api/users/{id}` - Actualizar usuario (Admin)
- `DELETE /api/users/{id}` - Eliminar usuario (Admin)

## Modelo de Datos

### Celular
```json
{
  "id": 1,
  "name": "iPhone 15",
  "description": "iPhone 15 con chip A17 Pro",
  "imageUrl": "https://ejemplo.com/iphone15.jpg",
  "variants": [
    {
      "id": 1,
      "color": "Negro",
      "price": 1200.00,
      "stock": 50
    }
  ]
}
```

### Usuario
```json
{
  "id": 1,
  "name": "Juan Pérez",
  "email": "juan@ejemplo.com",
  "role": 2,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

## Seguridad

- Autenticación JWT con tokens seguros
- Hash de contraseñas con BCrypt
- Autorización basada en roles
- Validación de datos en frontend y backend
- CORS configurado para desarrollo

## Patrones de Diseño Implementados

- **Repository Pattern**: Para acceso a datos
- **Dependency Injection**: En toda la aplicación
- **Observer Pattern**: Para manejo de estado en Angular
- **Factory Pattern**: Para creación de formularios dinámicos

## Buenas Prácticas

### Backend
- Separación de responsabilidades
- Validación de modelos
- Manejo centralizado de errores
- Configuración por entornos
- Documentación con Swagger

### Frontend
- Arquitectura por componentes
- Servicios reutilizables
- Reactive Forms
- Interceptores HTTP
- Tipado fuerte con TypeScript

## Desarrollo

Para desarrollo local, ambas aplicaciones deben ejecutarse simultáneamente:

1. Backend en `https://localhost:7000`
2. Frontend en `http://localhost:4200`

El frontend está configurado para comunicarse automáticamente con el backend local.

---

**Desarrollado para prueba técnica - Sistema completo de gestión de tienda de celulares**