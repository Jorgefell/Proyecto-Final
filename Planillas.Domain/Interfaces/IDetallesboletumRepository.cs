using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IDetallesboletumRepository : IGenericRepository<Detallesboletum>
{
    Task<IEnumerable<Detallesboletum>> GetByBoletaPagoIdAsync(int boletaPagoId, CancellationToken cancellationToken = default);
}
