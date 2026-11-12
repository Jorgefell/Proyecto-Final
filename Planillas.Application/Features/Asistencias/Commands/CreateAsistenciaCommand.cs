using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Asistencias.Commands;

public record CreateAsistenciaCommand(
    int TrabajadorId,
    int ObraId,
    DateTime Fecha,
    string Estado,
    decimal? HorasTrabajadas,
    decimal? HorasExtras,
    int? MinutosTardanza,
    string? Observaciones
) : IRequest<AsistenciaDto>;

public class CreateAsistenciaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAsistenciaCommand, AsistenciaDto>
{
    public async Task<AsistenciaDto> Handle(CreateAsistenciaCommand request, CancellationToken cancellationToken)
    {
        var asistencia = new Asistencia
        {
            TrabajadorId = request.TrabajadorId,
            ObraId = request.ObraId,
            Fecha = request.Fecha,
            Estado = request.Estado,
            HorasTrabajadas = request.HorasTrabajadas,
            HorasExtras = request.HorasExtras,
            MinutosTardanza = request.MinutosTardanza,
            Observaciones = request.Observaciones
        };

        var created = await unitOfWork.Asistencias.AddAsync(asistencia, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AsistenciaDto(
            created.Id,
            created.TrabajadorId,
            created.ObraId,
            created.Fecha,
            created.Estado,
            created.HorasTrabajadas,
            created.HorasExtras,
            created.MinutosTardanza,
            created.Observaciones
        );
    }
}
