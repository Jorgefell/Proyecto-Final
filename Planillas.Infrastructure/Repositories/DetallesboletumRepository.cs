using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class DetallesboletumRepository(ApplicationDbContext context)
    : GenericRepository<Detallesboletum>(context), IDetallesboletumRepository
{
    public async Task<IEnumerable<Detallesboletum>> GetByBoletaPagoIdAsync(int boletaPagoId, CancellationToken cancellationToken = default)
    {
        return await context.Detallesboleta
            .AsNoTracking()
            .Where(d => d.BoletaPagoId == boletaPagoId)
            .ToListAsync(cancellationToken);
    }
}
