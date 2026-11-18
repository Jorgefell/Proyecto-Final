using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetByUsernameWithRoleAsync(string username);
    Task<bool> ExistsByUsernameAsync(string username);
}