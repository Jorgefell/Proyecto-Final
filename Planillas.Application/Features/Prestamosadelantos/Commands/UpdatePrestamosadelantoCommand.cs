using MediatR;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Prestamosadelantos.Commands;

public record UpdatePrestamosadelantoCommand(
    int Id,
    int TrabajadorId,
    string Tipo,
    decimal MontoTotal,
    DateTime FechaSolicitud,
    string? Motivo,
    int? NumeroCuotas,
    decimal MontoPorCuota,
    string? Estado
) : IRequest<bool>;

public class UpdatePrestamosadelantoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePrestamosadelantoCommand, bool>
{
    public async Task<bool> Handle(UpdatePrestamosadelantoCommand request, CancellationToken cancellationToken)
    {
        var prestamo = await unitOfWork.Prestamosadelantos.GetByIdAsync(request.Id, cancellationToken);
        if (prestamo is null) return false;

        prestamo.TrabajadorId = request.TrabajadorId;
        prestamo.Tipo = request.Tipo;
        prestamo.MontoTotal = request.MontoTotal;
        prestamo.FechaSolicitud = request.FechaSolicitud;
        prestamo.Motivo = request.Motivo;
        prestamo.NumeroCuotas = request.NumeroCuotas;
        prestamo.MontoPorCuota = request.MontoPorCuota;
        prestamo.Estado = request.Estado;

        await unitOfWork.Prestamosadelantos.UpdateAsync(prestamo, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
