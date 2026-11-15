namespace Planillas.Application.DTOs;

public record BoletaspagoDto(
    int Id,
    int PlanillaId,
    int TrabajadorId,
    string TrabajadorNombreCompleto,
    string TrabajadorDocumento,
    string? CargoNombre,
    int DiasLaborados,
    int? DiasFalta,
    decimal? HorasExtrasTotales,
    decimal RemuneracionBasica,
    decimal TotalIngresos,
    decimal TotalDescuentos,
    decimal? TotalAportesEmpresa,
    decimal NetoPagar,
    IEnumerable<DetallesboletumDto> Detalles
);
