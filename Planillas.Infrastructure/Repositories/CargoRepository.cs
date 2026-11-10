using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class CargoRepository(ApplicationDbContext context) : ICargoRepository
{
    public async Task<IEnumerable<Cargo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Cargos.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Cargo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Cargos.FindAsync([id], cancellationToken);
    }

    public async Task<Cargo> AddAsync(Cargo cargo, CancellationToken cancellationToken = default)
    {
        await context.Cargos.AddAsync(cargo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return cargo;
    }

    public async Task UpdateAsync(Cargo cargo, CancellationToken cancellationToken = default)
    {
        context.Cargos.Update(cargo);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var cargo = await context.Cargos.FindAsync([id], cancellationToken);
        if (cargo is not null)
        {
            context.Cargos.Remove(cargo);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}