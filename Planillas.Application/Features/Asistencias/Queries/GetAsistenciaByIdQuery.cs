using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Asistencias.Queries;

public record GetAsistenciaByIdQuery(int Id) : IRequest<AsistenciaDto?>;

public class GetAsistenciaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAsistenciaByIdQuery, AsistenciaDto?>
{
    public async Task<AsistenciaDto?> Handle(GetAsistenciaByIdQuery request, CancellationToken cancellationToken)
    {
        var asistencia = await unitOfWork.Asistencias.GetByIdAsync(request.Id, cancellationToken);
        if (asistencia is null) return null;

        return new AsistenciaDto(
            asistencia.Id,
            asistencia.TrabajadorId,
            asistencia.ObraId,
            asistencia.Fecha,
            asistencia.Estado,
            asistencia.HorasTrabajadas,
            asistencia.HorasExtras,
            asistencia.MinutosTardanza,
            asistencia.Observaciones
        );
    }
}
