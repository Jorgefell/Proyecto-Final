using Microsoft.OpenApi.Models;

namespace Planillas.API.Extensions;

/// <summary>
/// Registra los servicios propios de la capa API, como Swagger,
/// manteniendo limpio el archivo Program.cs.
/// </summary>
public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Planillas API",
                Version = "v1",
                Description = "API de gestión de planillas de construcción."
            });
        });

        return services;
    }

    public static IApplicationBuilder UseApiServices(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Planillas API v1");
        });

        return app;
    }
}
