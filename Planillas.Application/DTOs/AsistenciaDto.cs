namespace Planillas.Application.DTOs;

public record AsistenciaDto(
    int Id,
    int TrabajadorId,
    int ObraId,
    DateTime Fecha,
    string Estado,
    decimal? HorasTrabajadas,
    decimal? HorasExtras,
    int? MinutosTardanza,
    string? Observaciones
);
