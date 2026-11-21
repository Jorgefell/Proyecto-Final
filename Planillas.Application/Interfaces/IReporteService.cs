using Planillas.Application.DTOs;

namespace Planillas.Application.Interfaces;

public interface IReporteService
{
    Task<IEnumerable<CostoPorObraDto>> GetCostosPorObraAsync(DateTime? fechaInicio, DateTime? fechaFin);
    Task<ResumenPlanillaDto?> GetResumenPlanillaAsync(int planillaId);
    Task<BoletaIndividualDto?> GetBoletaIndividualAsync(int boletaId);
}