using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IPlanillaRepository : IGenericRepository<Planilla>
{
    Task<IEnumerable<Planilla>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<Planilla?> GetByIdWithBoletasAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExisteCodigoAsync(string codigo, CancellationToken cancellationToken = default);
    Task<bool> ExistePlanillaEnPeriodoAsync(int? obraId, string tipo, DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default);
}
