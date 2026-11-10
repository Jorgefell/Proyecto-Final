namespace Planillas.Application.DTOs;

public record ObraDto(
    int Id,
    string Nombre,
    string Ubicacion,
    DateTime FechaInicio,
    DateTime? FechaFinEstimada,
    string? Estado,
    decimal? PresupuestoManoObra
);