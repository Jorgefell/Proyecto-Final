using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Trabajadores.Queries;

// QUERY
public record GetTrabajadorByIdQuery(int Id) : IRequest<TrabajadorDto?>;

// HANDLER
internal sealed class GetTrabajadorByIdQueryHandler : IRequestHandler<GetTrabajadorByIdQuery, TrabajadorDto?>
{
    private readonly ITrabajadorRepository _repository;

    public GetTrabajadorByIdQueryHandler(ITrabajadorRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrabajadorDto?> Handle(GetTrabajadorByIdQuery request, CancellationToken cancellationToken)
    {
        var trabajador = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (trabajador is null)
            return null;

        return new TrabajadorDto
        {
            Id = trabajador.Id,
            NumeroDocumento = trabajador.NumeroDocumento,
            Nombres = trabajador.Nombres,
            Apellidos = trabajador.Apellidos,
            TipoContrato = trabajador.TipoContrato,
            ModalidadPago = trabajador.ModalidadPago,
            RemuneracionBase = trabajador.RemuneracionBase,
            CargoId = trabajador.CargoId,
            CargoNombre = trabajador.Cargo?.Nombre,
            ObraActualId = trabajador.ObraActualId,
            FechaIngreso = trabajador.FechaIngreso,
            EstadoLaboral = trabajador.EstadoLaboral
        };
    }
}
