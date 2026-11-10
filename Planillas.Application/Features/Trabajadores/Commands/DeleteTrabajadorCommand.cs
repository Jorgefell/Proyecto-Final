using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Trabajadores.Commands;

public record DeleteTrabajadorCommand(int Id) : IRequest<bool>;

public class DeleteTrabajadorCommandHandler(ITrabajadorRepository repository)
    : IRequestHandler<DeleteTrabajadorCommand, bool>
{
    public async Task<bool> Handle(DeleteTrabajadorCommand request, CancellationToken cancellationToken)
    {
        var trabajador = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (trabajador is null) return false;

        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}