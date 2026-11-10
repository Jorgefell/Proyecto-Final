namespace Planillas.Domain.Interfaces;

/// <summary>
/// Repositorio genérico que define las operaciones CRUD comunes a todas las entidades.
/// Las consultas específicas se declaran en los repositorios personalizados.
/// </summary>
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
