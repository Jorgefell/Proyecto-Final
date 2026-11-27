using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
namespace Planillas.Application.Features.Trabajadores.Commands;
public record CreateTrabajadorCommand(
    string NumeroDocumento,
    string Nombres,
    string Apellidos,
    string TipoContrato,
    string ModalidadPago,
    decimal RemuneracionBase,
    int CargoId,
    int? ObraActualId
) : IRequest<TrabajadorDto>;
internal sealed class CreateTrabajadorCommandHandler : IRequestHandler<CreateTrabajadorCommand, TrabajadorDto>
{
    private readonly ITrabajadorRepository _repository;
    public CreateTrabajadorCommandHandler(ITrabajadorRepository repository)
    {
        _repository = repository;
    }
    public async Task<TrabajadorDto> Handle(CreateTrabajadorCommand request, CancellationToken cancellationToken)
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
        return new TrabajadorDto(
            trabajador.Id,
            trabajador.TipoDocumento,
            trabajador.NumeroDocumento,
            trabajador.Nombres,
            trabajador.Apellidos,
            trabajador.Telefono,
            trabajador.CargoId,
            trabajador.ObraActualId,
            trabajador.FechaIngreso,
            trabajador.FechaCese,
            trabajador.TipoContrato,
            trabajador.ModalidadPago,
            trabajador.RemuneracionBase,
            trabajador.EstadoLaboral
        );
    }
}