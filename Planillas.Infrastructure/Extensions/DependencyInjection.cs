using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planillas.Application.Interfaces;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;
using Planillas.Infrastructure.Repositories;
using Planillas.Infrastructure.Services;

namespace Planillas.Infrastructure.Extensions;

/// <summary>
/// Centraliza el registro de las dependencias de infraestructura:
/// DbContext, Unit of Work y repositorios.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connectionString!));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICargoRepository, CargoRepository>();
        services.AddScoped<IObraRepository, ObraRepository>();
        services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();
        services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
        services.AddScoped<IPrestamosadelantoRepository, PrestamosadelantoRepository>();
        services.AddScoped<IPlanillaRepository, PlanillaRepository>();
        services.AddScoped<IBoletaspagoRepository, BoletaspagoRepository>();
        services.AddScoped<IDetallesboletumRepository, DetallesboletumRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
