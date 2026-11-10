using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Obras.Commands;

public record UpdateObraCommand(
    int Id,
    string Nombre,
    string Ubicacion,
    DateTime FechaInicio,
    DateTime? FechaFinEstimada,
    string? Estado,
    decimal? PresupuestoManoObra
) : IRequest<bool>;

public class UpdateObraCommandHandler(IObraRepository repository)
    : IRequestHandler<UpdateObraCommand, bool>
{
    public async Task<bool> Handle(UpdateObraCommand request, CancellationToken cancellationToken)
    {
        var obra = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (obra is null) return false;

        obra.Nombre = request.Nombre;
        obra.Ubicacion = request.Ubicacion;
        obra.FechaInicio = request.FechaInicio;
        obra.FechaFinEstimada = request.FechaFinEstimada;
        obra.Estado = request.Estado;
        obra.PresupuestoManoObra = request.PresupuestoManoObra;

        await repository.UpdateAsync(obra, cancellationToken);
        return true;
    }
}