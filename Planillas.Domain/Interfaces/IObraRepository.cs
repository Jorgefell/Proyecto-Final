using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IObraRepository
{
    Task<IEnumerable<Obra>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Obra?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Obra> AddAsync(Obra obra, CancellationToken cancellationToken = default);
    Task UpdateAsync(Obra obra, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}