using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Cargos.Commands;

public record CreateCargoCommand(
    string Nombre,
    string? Area,
    decimal? SueldoBaseReferencial
) : IRequest<CargoDto>;

public class CreateCargoCommandHandler(ICargoRepository repository)
    : IRequestHandler<CreateCargoCommand, CargoDto>
{
    public async Task<CargoDto> Handle(CreateCargoCommand request, CancellationToken cancellationToken)
    {
        var cargo = new Cargo
        {
            Nombre = request.Nombre,
            Area = request.Area,
            SueldoBaseReferencial = request.SueldoBaseReferencial
        };

        var created = await repository.AddAsync(cargo, cancellationToken);

        return new CargoDto(
            created.Id,
            created.Nombre,
            created.Area,
            created.SueldoBaseReferencial
        );
    }
}