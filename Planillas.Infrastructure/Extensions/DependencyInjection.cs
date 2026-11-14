using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;
using Planillas.Infrastructure.Repositories;

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

        return services;
    }
}
