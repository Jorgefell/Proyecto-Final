using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Cargos.Commands;

public record UpdateCargoCommand(
    int Id,
    string Nombre,
    string? Area,
    decimal? SueldoBaseReferencial
) : IRequest<bool>;

public class UpdateCargoCommandHandler(ICargoRepository repository)
    : IRequestHandler<UpdateCargoCommand, bool>
{
    public async Task<bool> Handle(UpdateCargoCommand request, CancellationToken cancellationToken)
    {
        var cargo = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (cargo is null) return false;

        cargo.Nombre = request.Nombre;
        cargo.Area = request.Area;
        cargo.SueldoBaseReferencial = request.SueldoBaseReferencial;

        await repository.UpdateAsync(cargo, cancellationToken);
        return true;
    }
}