using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;

namespace Planillas.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var nombresRoles = new[] { "Admin", "Contador", "RRHH" };
        foreach (var nombre in nombresRoles)
        {
            if (!await context.Roles.AnyAsync(r => r.Nombre == nombre))
            {
                context.Roles.Add(new Role { Nombre = nombre });
            }
        }
        await context.SaveChangesAsync();

        var adminRol = await context.Roles.FirstAsync(r => r.Nombre == "Admin");
        var adminUser = await context.Usuarios.FirstOrDefaultAsync(u => u.Username == "admin");

        if (adminUser == null)
        {
            context.Usuarios.Add(new Usuario
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                NombreCompleto = "Administrador General",
                RolId = adminRol.Id,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            });
        }
        else
        {
            // Asegura que el hash sea válido para BCrypt si venía en texto plano
            adminUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            adminUser.RolId = adminRol.Id;
            adminUser.Activo = true;
        }

        await context.SaveChangesAsync();
    }
}