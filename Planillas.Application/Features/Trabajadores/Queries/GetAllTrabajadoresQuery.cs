using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Trabajadores.Queries;

public record GetAllTrabajadoresQuery : IRequest<IEnumerable<TrabajadorDto>>;

public class GetAllTrabajadoresQueryHandler(ITrabajadorRepository repository)
    : IRequestHandler<GetAllTrabajadoresQuery, IEnumerable<TrabajadorDto>>
{
    public async Task<IEnumerable<TrabajadorDto>> Handle(GetAllTrabajadoresQuery request, CancellationToken cancellationToken)
    {
        var trabajadores = await repository.GetAllAsync(cancellationToken);

        return trabajadores.Select(t => new TrabajadorDto(
            t.Id,
            t.TipoDocumento,
            t.NumeroDocumento,
            t.Nombres,
            t.Apellidos,
            t.Telefono,
            t.CargoId,
            t.ObraActualId,
            t.FechaIngreso,
            t.FechaCese,
            t.TipoContrato,
            t.ModalidadPago,
            t.RemuneracionBase,
            t.EstadoLaboral
        ));
    }
}