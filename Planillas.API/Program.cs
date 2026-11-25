using Planillas.API.Extensions;
using Planillas.Application.Extensions;
using Planillas.Infrastructure.Extensions;
using Planillas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<Planillas.API.Middleware.ExceptionMiddleware>();
app.UseCors("AllowFrontend");

app.UseApiServices();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DbInitializer.SeedAsync(context);
}

app.Run();