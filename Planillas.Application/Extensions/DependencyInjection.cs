using Microsoft.Extensions.DependencyInjection;

namespace Planillas.Application.Extensions;

/// <summary>
/// Centraliza el registro de los servicios de la capa de aplicación.
/// MediatR se registra mediante el ensamblado de Application para descubrir automáticamente
/// todos los Use Cases (Commands y Queries).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
