using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Asistencias.Commands;

public record UpdateAsistenciaCommand(
    int Id,
    int TrabajadorId,
    int ObraId,
    DateTime Fecha,
    string Estado,
    decimal? HorasTrabajadas,
    decimal? HorasExtras,
    int? MinutosTardanza,
    string? Observaciones
) : IRequest<bool>;

public class UpdateAsistenciaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAsistenciaCommand, bool>
{
    public async Task<bool> Handle(UpdateAsistenciaCommand request, CancellationToken cancellationToken)
    {
        var asistencia = await unitOfWork.Asistencias.GetByIdAsync(request.Id, cancellationToken);
        if (asistencia is null) return false;

        asistencia.TrabajadorId = request.TrabajadorId;
        asistencia.ObraId = request.ObraId;
        asistencia.Fecha = request.Fecha;
        asistencia.Estado = request.Estado;
        asistencia.HorasTrabajadas = request.HorasTrabajadas;
        asistencia.HorasExtras = request.HorasExtras;
        asistencia.MinutosTardanza = request.MinutosTardanza;
        asistencia.Observaciones = request.Observaciones;

        await unitOfWork.Asistencias.UpdateAsync(asistencia, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
