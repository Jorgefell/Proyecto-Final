using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface ITrabajadorRepository
{
    Task<IEnumerable<Trabajadore>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Trabajadore?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Trabajadore> AddAsync(Trabajadore trabajador, CancellationToken cancellationToken = default);
    Task UpdateAsync(Trabajadore trabajador, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}