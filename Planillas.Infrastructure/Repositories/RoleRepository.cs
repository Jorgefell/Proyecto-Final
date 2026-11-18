using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNombreAsync(string nombre)
    {
        return await _context.Set<Role>()
            .FirstOrDefaultAsync(r => r.Nombre == nombre);
    }
}