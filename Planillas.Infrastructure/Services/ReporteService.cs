using Microsoft.EntityFrameworkCore;
using Planillas.Application.DTOs;
using Planillas.Application.Interfaces;
using Planillas.Infrastructure.Persistence;
namespace Planillas.Infrastructure.Services;
public class ReporteService : IReporteService
{
    private readonly ApplicationDbContext _context;
    public ReporteService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CostoPorObraDto>> GetCostosPorObraAsync(DateTime? fechaInicio, DateTime? fechaFin)
    {
        var query = _context.Planillas
            .Include(p => p.Obra)
            .Include(p => p.Boletaspagos)
            .AsQueryable();
        if (fechaInicio.HasValue)
            query = query.Where(p => p.FechaInicio >= fechaInicio.Value);
        if (fechaFin.HasValue)
            query = query.Where(p => p.FechaFin <= fechaFin.Value);
        var planillas = await query.ToListAsync();

        return planillas
            .GroupBy(p => new { p.ObraId, Nombre = p.Obra?.Nombre ?? "Sin Obra Asignada" })
            .Select(g => new CostoPorObraDto(
                g.Key.ObraId,
                g.Key.Nombre,
                g.Count(),
                g.SelectMany(p => p.Boletaspagos).Select(b => b.TrabajadorId).Distinct().Count(),
                g.SelectMany(p => p.Boletaspagos).Sum(b => b.NetoPagar)
            ))
            .ToList();
    }
    public async Task<ResumenPlanillaDto?> GetResumenPlanillaAsync(int planillaId)
    {
        var planilla = await _context.Planillas
            .Include(p => p.Obra)
            .Include(p => p.Boletaspagos)
            .FirstOrDefaultAsync(p => p.Id == planillaId);
        if (planilla == null) return null;
        return new ResumenPlanillaDto(
            planilla.Id,
            planilla.Codigo,
            planilla.Obra?.Nombre ?? "Sin Obra Asignada",
            planilla.FechaInicio,
            planilla.FechaFin,
            planilla.Estado ?? "Pendiente",
            planilla.Boletaspagos.Count,
            planilla.Boletaspagos.Sum(b => b.TotalIngresos),
            planilla.Boletaspagos.Sum(b => b.TotalDescuentos),
            planilla.Boletaspagos.Sum(b => b.NetoPagar)
        );
    }
    public async Task<BoletaIndividualDto?> GetBoletaIndividualAsync(int boletaId)
    {
        var boleta = await _context.Boletaspagos
            .Include(b => b.Trabajador)
                .ThenInclude(t => t.Cargo)
            .Include(b => b.Detallesboleta)
            .FirstOrDefaultAsync(b => b.Id == boletaId);
        if (boleta == null) return null;
        var trabajador = boleta.Trabajador;
        var detalles = boleta.Detallesboleta
            .Select(d => new DetalleConceptoDto(d.Concepto, d.Tipo, d.Monto))
            .ToList();
        return new BoletaIndividualDto(
            boleta.Id,
            boleta.PlanillaId,
            $"{trabajador?.Nombres} {trabajador?.Apellidos}".Trim(),
            trabajador?.NumeroDocumento ?? string.Empty,
            trabajador?.Cargo?.Nombre ?? "Sin Cargo",
            boleta.DiasLaborados,
            boleta.TotalIngresos,
            boleta.TotalDescuentos,
            boleta.NetoPagar,
            detalles
        );
    }
}