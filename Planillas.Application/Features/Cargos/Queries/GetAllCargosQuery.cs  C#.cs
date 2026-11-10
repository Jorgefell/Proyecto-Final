using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Cargos.Queries;

public record GetAllCargosQuery : IRequest<IEnumerable<CargoDto>>;

public class GetAllCargosQueryHandler(ICargoRepository repository)
    : IRequestHandler<GetAllCargosQuery, IEnumerable<CargoDto>>
{
    public async Task<IEnumerable<CargoDto>> Handle(GetAllCargosQuery request, CancellationToken cancellationToken)
    {
        var cargos = await repository.GetAllAsync(cancellationToken);

        return cargos.Select(c => new CargoDto(
            c.Id,
            c.Nombre,
            c.Area,
            c.SueldoBaseReferencial
        ));
    }
}