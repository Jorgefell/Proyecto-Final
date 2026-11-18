using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Queries;

public record GetAllPlanillasQuery : IRequest<IEnumerable<PlanillaResumenDto>>;

public class GetAllPlanillasQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllPlanillasQuery, IEnumerable<PlanillaResumenDto>>
{
    public async Task<IEnumerable<PlanillaResumenDto>> Handle(GetAllPlanillasQuery request, CancellationToken cancellationToken)
    {
        var planillas = await unitOfWork.Planillas.GetAllWithDetailsAsync(cancellationToken);

        return planillas.Select(p => new PlanillaResumenDto(
            p.Id,
            p.Codigo,
            p.ObraId,
            p.Obra?.Nombre,
            p.Tipo,
            p.FechaInicio,
            p.FechaFin,
            p.Estado,
            p.UsuarioProcesoId,
            p.FechaCreacion,
            p.Boletaspagos.Count,
            p.Boletaspagos.Sum(b => b.NetoPagar)
        ));
    }
}
