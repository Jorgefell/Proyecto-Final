using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Queries;

public record GetBoletasByPlanillaIdQuery(int PlanillaId) : IRequest<IEnumerable<BoletaspagoDto>>;

public class GetBoletasByPlanillaIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetBoletasByPlanillaIdQuery, IEnumerable<BoletaspagoDto>>
{
    public async Task<IEnumerable<BoletaspagoDto>> Handle(GetBoletasByPlanillaIdQuery request, CancellationToken cancellationToken)
    {
        var boletas = await unitOfWork.Boletaspagos.GetByPlanillaIdAsync(request.PlanillaId, cancellationToken);

        return boletas.Select(b => new BoletaspagoDto(
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
        ));
    }
}
