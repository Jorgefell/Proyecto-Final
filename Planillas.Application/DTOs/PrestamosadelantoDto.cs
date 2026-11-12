namespace Planillas.Application.DTOs;

public record PrestamosadelantoDto(
    int Id,
    int TrabajadorId,
    string Tipo,
    decimal MontoTotal,
    DateTime FechaSolicitud,
    string? Motivo,
    int? NumeroCuotas,
    decimal MontoPorCuota,
    string? Estado
);
