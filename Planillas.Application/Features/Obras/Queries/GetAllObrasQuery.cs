using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Obras.Queries;

public record GetAllObrasQuery : IRequest<IEnumerable<ObraDto>>;

public class GetAllObrasQueryHandler(IObraRepository repository)
    : IRequestHandler<GetAllObrasQuery, IEnumerable<ObraDto>>
{
    public async Task<IEnumerable<ObraDto>> Handle(GetAllObrasQuery request, CancellationToken cancellationToken)
    {
        var obras = await repository.GetAllAsync(cancellationToken);

        return obras.Select(o => new ObraDto(
            o.Id,
            o.Nombre,
            o.Ubicacion,
            o.FechaInicio,
            o.FechaFinEstimada,
            o.Estado,
            o.PresupuestoManoObra
        ));
    }
}