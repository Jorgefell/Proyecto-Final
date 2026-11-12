using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Prestamosadelantos.Commands;

public record DeletePrestamosadelantoCommand(int Id) : IRequest<bool>;

public class DeletePrestamosadelantoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePrestamosadelantoCommand, bool>
{
    public async Task<bool> Handle(DeletePrestamosadelantoCommand request, CancellationToken cancellationToken)
    {
        var prestamo = await unitOfWork.Prestamosadelantos.GetByIdAsync(request.Id, cancellationToken);
        if (prestamo is null) return false;

        await unitOfWork.Prestamosadelantos.DeleteAsync(request.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
