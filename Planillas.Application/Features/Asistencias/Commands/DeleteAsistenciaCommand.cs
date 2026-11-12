using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Asistencias.Commands;

public record DeleteAsistenciaCommand(int Id) : IRequest<bool>;

public class DeleteAsistenciaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAsistenciaCommand, bool>
{
    public async Task<bool> Handle(DeleteAsistenciaCommand request, CancellationToken cancellationToken)
    {
        var asistencia = await unitOfWork.Asistencias.GetByIdAsync(request.Id, cancellationToken);
        if (asistencia is null) return false;

        await unitOfWork.Asistencias.DeleteAsync(request.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
