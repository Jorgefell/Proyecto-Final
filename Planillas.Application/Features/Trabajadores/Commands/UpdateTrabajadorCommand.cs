using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Trabajadores.Commands;

public record UpdateTrabajadorCommand(
    int Id,
    string NumeroDocumento,
    string Nombres,
    string Apellidos,
    string? Telefono,
    int CargoId,
    int? ObraActualId,
    DateTime FechaIngreso,
    string? EstadoLaboral
) : IRequest<bool>;

public class UpdateTrabajadorCommandHandler(ITrabajadorRepository repository)
    : IRequestHandler<UpdateTrabajadorCommand, bool>
{
    public async Task<bool> Handle(UpdateTrabajadorCommand request, CancellationToken cancellationToken)
    {
        var trabajador = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (trabajador is null) return false;

        trabajador.NumeroDocumento = request.NumeroDocumento;
        trabajador.Nombres = request.Nombres;
        trabajador.Apellidos = request.Apellidos;
        trabajador.Telefono = request.Telefono;
        trabajador.CargoId = request.CargoId;
        trabajador.ObraActualId = request.ObraActualId;
        trabajador.FechaIngreso = request.FechaIngreso;
        trabajador.EstadoLaboral = request.EstadoLaboral;

        await repository.UpdateAsync(trabajador, cancellationToken);
        return true;
    }
}