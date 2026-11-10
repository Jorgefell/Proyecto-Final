using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Obras.Commands;

public record DeleteObraCommand(int Id) : IRequest<bool>;

public class DeleteObraCommandHandler(IObraRepository repository)
    : IRequestHandler<DeleteObraCommand, bool>
{
    public async Task<bool> Handle(DeleteObraCommand request, CancellationToken cancellationToken)
    {
        var obra = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (obra is null) return false;

        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}