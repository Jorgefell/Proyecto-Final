using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Obras.Queries;

public record GetObraByIdQuery(int Id) : IRequest<ObraDto?>;

public class GetObraByIdQueryHandler(IObraRepository repository)
    : IRequestHandler<GetObraByIdQuery, ObraDto?>
{
    public async Task<ObraDto?> Handle(GetObraByIdQuery request, CancellationToken cancellationToken)
    {
        var obra = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (obra is null) return null;

        return new ObraDto(
            obra.Id,
            obra.Nombre,
            obra.Ubicacion,
            obra.FechaInicio,
            obra.FechaFinEstimada,
            obra.Estado,
            obra.PresupuestoManoObra
        );
    }
}