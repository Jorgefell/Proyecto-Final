using MediatR;
using Planillas.Application.Features.Trabajadores.Commands;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;
using Planillas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(CreateTrabajadorCommand).Assembly));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
