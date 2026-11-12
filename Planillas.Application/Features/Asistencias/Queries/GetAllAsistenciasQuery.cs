using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Asistencias.Queries;

public record GetAllAsistenciasQuery : IRequest<IEnumerable<AsistenciaDto>>;

public class GetAllAsistenciasQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllAsistenciasQuery, IEnumerable<AsistenciaDto>>
{
    public async Task<IEnumerable<AsistenciaDto>> Handle(GetAllAsistenciasQuery request, CancellationToken cancellationToken)
    {
        var asistencias = await unitOfWork.Asistencias.GetAllAsync(cancellationToken);

        return asistencias.Select(a => new AsistenciaDto(
            a.Id,
            a.TrabajadorId,
            a.ObraId,
            a.Fecha,
            a.Estado,
            a.HorasTrabajadas,
            a.HorasExtras,
            a.MinutosTardanza,
            a.Observaciones
        ));
    }
}
