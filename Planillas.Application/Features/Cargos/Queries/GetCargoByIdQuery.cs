using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Cargos.Queries;

public record GetCargoByIdQuery(int Id) : IRequest<CargoDto?>;

public class GetCargoByIdQueryHandler(ICargoRepository repository)
    : IRequestHandler<GetCargoByIdQuery, CargoDto?>
{
    public async Task<CargoDto?> Handle(GetCargoByIdQuery request, CancellationToken cancellationToken)
    {
        var cargo = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (cargo is null) return null;

        return new CargoDto(
            cargo.Id,
            cargo.Nombre,
            cargo.Area,
            cargo.SueldoBaseReferencial
        );
    }
}