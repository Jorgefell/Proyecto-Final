using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Prestamosadelantos.Queries;

public record GetPrestamosadelantoByIdQuery(int Id) : IRequest<PrestamosadelantoDto?>;

public class GetPrestamosadelantoByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPrestamosadelantoByIdQuery, PrestamosadelantoDto?>
{
    public async Task<PrestamosadelantoDto?> Handle(GetPrestamosadelantoByIdQuery request, CancellationToken cancellationToken)
    {
        var prestamo = await unitOfWork.Prestamosadelantos.GetByIdAsync(request.Id, cancellationToken);
        if (prestamo is null) return null;

        return new PrestamosadelantoDto(
            prestamo.Id,
            prestamo.TrabajadorId,
            prestamo.Tipo,
            prestamo.MontoTotal,
            prestamo.FechaSolicitud,
            prestamo.Motivo,
            prestamo.NumeroCuotas,
            prestamo.MontoPorCuota,
            prestamo.Estado
        );
    }
}
