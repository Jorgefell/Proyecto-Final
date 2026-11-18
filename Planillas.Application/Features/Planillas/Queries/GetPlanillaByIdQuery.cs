using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Queries;

public record GetPlanillaByIdQuery(int Id) : IRequest<PlanillaDetalleDto?>;

public class GetPlanillaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPlanillaByIdQuery, PlanillaDetalleDto?>
{
    public async Task<PlanillaDetalleDto?> Handle(GetPlanillaByIdQuery request, CancellationToken cancellationToken)
    {
        var planilla = await unitOfWork.Planillas.GetByIdWithBoletasAsync(request.Id, cancellationToken);
        if (planilla is null) return null;

        var boletas = planilla.Boletaspagos.Select(b => new BoletaspagoDto(
            b.Id,
            b.PlanillaId,
            b.TrabajadorId,
            $"{b.Trabajador.Nombres} {b.Trabajador.Apellidos}",
            b.Trabajador.NumeroDocumento,
            b.Trabajador.Cargo?.Nombre,
            b.DiasLaborados,
            b.DiasFalta,
            b.HorasExtrasTotales,
            b.RemuneracionBasica,
            b.TotalIngresos,
            b.TotalDescuentos,
            b.TotalAportesEmpresa,
            b.NetoPagar,
            b.Detallesboleta.Select(d => new DetallesboletumDto(
                d.Id,
                d.BoletaPagoId,
                d.Tipo,
                d.Concepto,
                d.Monto
            )).ToList()
        )).ToList();

        return new PlanillaDetalleDto(
            planilla.Id,
            planilla.Codigo,
            planilla.ObraId,
            planilla.Obra?.Nombre,
            planilla.Tipo,
            planilla.FechaInicio,
            planilla.FechaFin,
            planilla.Estado,
            planilla.UsuarioProcesoId,
            planilla.FechaCreacion,
            boletas.Sum(b => b.TotalIngresos),
            boletas.Sum(b => b.TotalDescuentos),
            boletas.Sum(b => b.TotalAportesEmpresa ?? 0m),
            boletas.Sum(b => b.NetoPagar),
            boletas
        );
    }
}
