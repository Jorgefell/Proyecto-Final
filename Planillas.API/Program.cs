using Planillas.API.Extensions;
using Planillas.Application.Extensions;
using Planillas.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Servicios de cada capa, registrados mediante métodos de extensión.
builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configuración del pipeline HTTP.
app.UseApiServices();

app.MapControllers();

app.Run();
