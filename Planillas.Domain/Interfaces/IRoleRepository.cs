using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNombreAsync(string nombre);
}