using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class TrabajadorRepository : ITrabajadorRepository
{
    private readonly ApplicationDbContext _context;

    public TrabajadorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Trabajadore trabajador, CancellationToken cancellationToken = default)
    {
        await _context.Trabajadores.AddAsync(trabajador, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Trabajadore?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Trabajadores
            .Include(t => t.Cargo)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Trabajadore>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Trabajadores
            .Include(t => t.Cargo)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}