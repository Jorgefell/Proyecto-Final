using MediatR;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Trabajadores.Commands;

// COMMAND
public record CreateTrabajadorCommand(
    string NumeroDocumento,
    string Nombres,
    string Apellidos,
    string TipoContrato,
    string ModalidadPago,
    decimal RemuneracionBase,
    int CargoId,
    int? ObraActualId
) : IRequest<int>;

// HANDLER
internal sealed class CreateTrabajadorCommandHandler : IRequestHandler<CreateTrabajadorCommand, int>
{
    private readonly ITrabajadorRepository _repository;

    public CreateTrabajadorCommandHandler(ITrabajadorRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateTrabajadorCommand request, CancellationToken cancellationToken)
    {
        var trabajador = new Trabajadore
        {
            NumeroDocumento = request.NumeroDocumento,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            TipoContrato = request.TipoContrato,
            ModalidadPago = request.ModalidadPago,
            RemuneracionBase = request.RemuneracionBase,
            CargoId = request.CargoId,
            ObraActualId = request.ObraActualId,
            FechaIngreso = DateTime.Today,
            EstadoLaboral = "Activo"
        };

        await _repository.AddAsync(trabajador, cancellationToken);
        return trabajador.Id;
    }
}