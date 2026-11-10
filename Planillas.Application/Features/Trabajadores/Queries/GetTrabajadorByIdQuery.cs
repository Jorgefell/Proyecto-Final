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
