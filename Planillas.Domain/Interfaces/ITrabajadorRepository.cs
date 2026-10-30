using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface ITrabajadorRepository
{
    Task AddAsync(Trabajadore trabajador, CancellationToken cancellationToken = default);
    Task<Trabajadore?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Trabajadore>> GetAllAsync(CancellationToken cancellationToken = default);
}