namespace Planillas.Application.DTOs;

public record CostoPorObraDto(
    int? ObraId,
    string NombreObra,
    int CantidadPlanillas,
    int TotalTrabajadores,
    decimal CostoTotalManoObra
);

public record ResumenPlanillaDto(
    int PlanillaId,
    string CodigoPlanilla,
    string NombreObra,
    DateTime FechaInicio,
    DateTime FechaFin,
    string Estado,
    int CantidadBoletas,
    decimal TotalIngresos,
    decimal TotalDescuentos,
    decimal TotalNetoPagar
);

public record DetalleConceptoDto(string Concepto, string Tipo, decimal Monto);

public record BoletaIndividualDto(
    int BoletaId,
    int PlanillaId,
    string TrabajadorNombre,
    string NumeroDocumento,
    string Cargo,
    int DiasLaborados,
    decimal TotalIngresos,
    decimal TotalDescuentos,
    decimal NetoPagar,
    List<DetalleConceptoDto> Detalles
);