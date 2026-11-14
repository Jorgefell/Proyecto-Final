using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

public class PlanillaRepository(ApplicationDbContext context)
    : GenericRepository<Planilla>(context), IPlanillaRepository
{
    public async Task<IEnumerable<Planilla>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Planillas
            .AsNoTracking()
            .Include(p => p.Obra)
            .Include(p => p.Boletaspagos)
            .OrderByDescending(p => p.FechaInicio)
            .ToListAsync(cancellationToken);
    }

    public async Task<Planilla?> GetByIdWithBoletasAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Planillas
            .AsNoTracking()
            .Include(p => p.Obra)
            .Include(p => p.Boletaspagos)
                .ThenInclude(b => b.Trabajador)
                    .ThenInclude(t => t.Cargo)
            .Include(p => p.Boletaspagos)
                .ThenInclude(b => b.Detallesboleta)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExisteCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        return await context.Planillas
            .AsNoTracking()
            .AnyAsync(p => p.Codigo == codigo, cancellationToken);
    }

    public async Task<bool> ExistePlanillaEnPeriodoAsync(int? obraId, string tipo, DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default)
    {
        return await context.Planillas
            .AsNoTracking()
            .AnyAsync(p => p.ObraId == obraId
                        && p.Tipo == tipo
                        && p.Estado != "ANULADA"
                        && p.FechaInicio <= fechaFin
                        && p.FechaFin >= fechaInicio, cancellationToken);
    }
}
