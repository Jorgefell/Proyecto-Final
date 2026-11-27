using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Enums;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Obras.Commands;

public record CreateObraCommand(
    string Nombre,
    string Ubicacion,
    DateTime FechaInicio,
    DateTime? FechaFinEstimada,
    string? Estado,
    decimal? PresupuestoManoObra
) : IRequest<ObraDto>;

public class CreateObraCommandHandler(IObraRepository repository)
    : IRequestHandler<CreateObraCommand, ObraDto>
{
    public async Task<ObraDto> Handle(CreateObraCommand request, CancellationToken cancellationToken)
    {
        var obra = new Obra
        {
            Nombre = request.Nombre,
            Ubicacion = request.Ubicacion,
            FechaInicio = request.FechaInicio,
            FechaFinEstimada = request.FechaFinEstimada,
            Estado = ObraEstado.Normalizar(request.Estado) ?? ObraEstado.Planificacion,
            PresupuestoManoObra = request.PresupuestoManoObra
        };

        var created = await repository.AddAsync(obra, cancellationToken);

        return new ObraDto(
            created.Id,
            created.Nombre,
            created.Ubicacion,
            created.FechaInicio,
            created.FechaFinEstimada,
            created.Estado,
            created.PresupuestoManoObra
        );
    }
}