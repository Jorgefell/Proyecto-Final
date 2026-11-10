using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface ICargoRepository
{
    Task<IEnumerable<Cargo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Cargo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Cargo> AddAsync(Cargo cargo, CancellationToken cancellationToken = default);
    Task UpdateAsync(Cargo cargo, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}