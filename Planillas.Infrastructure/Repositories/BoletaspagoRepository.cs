using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class BoletaspagoRepository(ApplicationDbContext context)
    : GenericRepository<Boletaspago>(context), IBoletaspagoRepository
{
    public async Task<IEnumerable<Boletaspago>> GetByPlanillaIdAsync(int planillaId, CancellationToken cancellationToken = default)
    {
        return await context.Boletaspagos
            .AsNoTracking()
            .Include(b => b.Trabajador)
                .ThenInclude(t => t.Cargo)
            .Include(b => b.Detallesboleta)
            .Where(b => b.PlanillaId == planillaId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Boletaspago?> GetByIdWithDetallesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Boletaspagos
            .AsNoTracking()
            .Include(b => b.Trabajador)
                .ThenInclude(t => t.Cargo)
            .Include(b => b.Planilla)
            .Include(b => b.Detallesboleta)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}
