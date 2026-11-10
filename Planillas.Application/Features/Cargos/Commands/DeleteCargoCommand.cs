using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Cargos.Commands;

public record DeleteCargoCommand(int Id) : IRequest<bool>;

public class DeleteCargoCommandHandler(ICargoRepository repository)
    : IRequestHandler<DeleteCargoCommand, bool>
{
    public async Task<bool> Handle(DeleteCargoCommand request, CancellationToken cancellationToken)
    {
        var cargo = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (cargo is null) return false;

        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}