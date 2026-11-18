using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Commands;

public record CambiarEstadoPlanillaCommand(int Id, string NuevoEstado) : IRequest<bool>;

public class CambiarEstadoPlanillaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CambiarEstadoPlanillaCommand, bool>
{
    public async Task<bool> Handle(CambiarEstadoPlanillaCommand request, CancellationToken cancellationToken)
    {
        var planilla = await unitOfWork.Planillas.GetByIdAsync(request.Id, cancellationToken);
        if (planilla is null) return false;

        var estado = request.NuevoEstado.Trim().ToUpperInvariant();
        if (estado is not ("PROCESADA" or "APROBADA" or "PAGADA" or "ANULADA"))
            throw new InvalidOperationException("Estado no válido. Use: PROCESADA, APROBADA, PAGADA o ANULADA.");

        planilla.Estado = estado;
        await unitOfWork.Planillas.UpdateAsync(planilla, cancellationToken);
        return true;
    }
}
