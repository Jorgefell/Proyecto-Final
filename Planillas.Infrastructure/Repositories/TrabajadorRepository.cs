using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class TrabajadorRepository(ApplicationDbContext context) : ITrabajadorRepository
{
    public async Task<IEnumerable<Trabajadore>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Trabajadores.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Trabajadore?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Trabajadores.FindAsync([id], cancellationToken);
    }

    public async Task<Trabajadore> AddAsync(Trabajadore trabajador, CancellationToken cancellationToken = default)
    {
        await context.Trabajadores.AddAsync(trabajador, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return trabajador;
    }

    public async Task UpdateAsync(Trabajadore trabajador, CancellationToken cancellationToken = default)
    {
        context.Trabajadores.Update(trabajador);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var trabajador = await context.Trabajadores.FindAsync([id], cancellationToken);
        if (trabajador is not null)
        {
            context.Trabajadores.Remove(trabajador);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}