using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;

namespace Planillas.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Role { Nombre = "Admin" },
                new Role { Nombre = "Contador" },
                new Role { Nombre = "RRHH" }
            );
            await context.SaveChangesAsync();
        }

        if (!await context.Usuarios.AnyAsync())
        {
            var adminRol = await context.Roles.FirstAsync(r => r.Nombre == "Admin");
            context.Usuarios.Add(new Usuario
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                NombreCompleto = "Administrador General",
                RolId = adminRol.Id,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
        }
    }
}