using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Prestamosadelantos.Commands;

public record CreatePrestamosadelantoCommand(
    int TrabajadorId,
    string Tipo,
    decimal MontoTotal,
    DateTime FechaSolicitud,
    string? Motivo,
    int? NumeroCuotas,
    decimal MontoPorCuota,
    string? Estado
) : IRequest<PrestamosadelantoDto>;

public class CreatePrestamosadelantoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePrestamosadelantoCommand, PrestamosadelantoDto>
{
    public async Task<PrestamosadelantoDto> Handle(CreatePrestamosadelantoCommand request, CancellationToken cancellationToken)
    {
        var prestamo = new Prestamosadelanto
        {
            TrabajadorId = request.TrabajadorId,
            Tipo = request.Tipo,
            MontoTotal = request.MontoTotal,
            FechaSolicitud = request.FechaSolicitud,
            Motivo = request.Motivo,
            NumeroCuotas = request.NumeroCuotas,
            MontoPorCuota = request.MontoPorCuota,
            Estado = request.Estado ?? "Pendiente"
        };

        var created = await unitOfWork.Prestamosadelantos.AddAsync(prestamo, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PrestamosadelantoDto(
            created.Id,
            created.TrabajadorId,
            created.Tipo,
            created.MontoTotal,
            created.FechaSolicitud,
            created.Motivo,
            created.NumeroCuotas,
            created.MontoPorCuota,
            created.Estado
        );
    }
}
