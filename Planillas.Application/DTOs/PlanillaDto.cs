namespace Planillas.Application.DTOs;

public record PlanillaResumenDto(
    int Id,
    string Codigo,
    int? ObraId,
    string? ObraNombre,
    string Tipo,
    DateTime FechaInicio,
    DateTime FechaFin,
    string? Estado,
    int UsuarioProcesoId,
    DateTime? FechaCreacion,
    int CantidadBoletas,
    decimal TotalNetoPagar
);

public record PlanillaDetalleDto(
    int Id,
    string Codigo,
    int? ObraId,
    string? ObraNombre,
    string Tipo,
    DateTime FechaInicio,
    DateTime FechaFin,
    string? Estado,
    int UsuarioProcesoId,
    DateTime? FechaCreacion,
    decimal TotalIngresosPlanilla,
    decimal TotalDescuentosPlanilla,
    decimal TotalAportesEmpresaPlanilla,
    decimal TotalNetoPagar,
    IEnumerable<BoletaspagoDto> Boletas
);
