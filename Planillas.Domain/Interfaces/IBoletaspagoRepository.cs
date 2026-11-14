using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IBoletaspagoRepository : IGenericRepository<Boletaspago>
{
    Task<IEnumerable<Boletaspago>> GetByPlanillaIdAsync(int planillaId, CancellationToken cancellationToken = default);
    Task<Boletaspago?> GetByIdWithDetallesAsync(int id, CancellationToken cancellationToken = default);
}
