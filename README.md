# Sistema Integral de Gestión de Planillas

API RESTful y cliente web ligero desarrollado en **.NET 10** bajo **Clean Architecture** para la gestión de trabajadores, obras, asistencias, adelantos y cálculo automatizado de planillas en el sector construcción.

## Arquitectura de la Solución
- **Planillas.Domain:** Entidades del dominio, enumeraciones, excepciones personalizadas e interfaces de repositorio.
- **Planillas.Application:** Casos de uso bajo patrón CQRS con MediatR, DTOs e interfaces de servicios.
- **Planillas.Infrastructure:** Persistencia con Entity Framework Core (MySQL), Fluent API, inicialización de datos (`DbInitializer`), repositorios y servicios JWT/Reportes.
- **Planillas.API:** Controladores REST protegidos con autenticación JWT, middleware global de excepciones y documentación Swagger.

## Tecnologías Utilizadas
- .NET 10 / ASP.NET Core Web API
- Entity Framework Core + MySQL (`Pomelo.EntityFrameworkCore.MySql`)
- MediatR (CQRS)
- Autenticación JWT + BCrypt.Net
- Swagger / OpenAPI

## Ejecución Rápida
1. Configurar la cadena de conexión `DefaultConnection` en `Planillas.API/appsettings.json`.
2. Ejecutar `dotnet run --project Planillas.API`.
3. Acceder a `http://localhost:5000/swagger` e iniciar sesión con las credenciales iniciales (`admin` / `Admin123!`).
