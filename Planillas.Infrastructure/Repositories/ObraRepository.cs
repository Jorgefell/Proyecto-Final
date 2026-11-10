using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class ObraRepository(ApplicationDbContext context) : IObraRepository
{
    public async Task<IEnumerable<Obra>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Obras.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Obra?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Obras.FindAsync([id], cancellationToken);
    }

    public async Task<Obra> AddAsync(Obra obra, CancellationToken cancellationToken = default)
    {
        await context.Obras.AddAsync(obra, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return obra;
    }

    public async Task UpdateAsync(Obra obra, CancellationToken cancellationToken = default)
    {
        context.Obras.Update(obra);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var obra = await context.Obras.FindAsync([id], cancellationToken);
        if (obra is not null)
        {
            context.Obras.Remove(obra);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}