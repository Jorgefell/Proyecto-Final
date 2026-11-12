using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Prestamosadelantos.Queries;

public record GetAllPrestamosadelantosQuery : IRequest<IEnumerable<PrestamosadelantoDto>>;

public class GetAllPrestamosadelantosQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllPrestamosadelantosQuery, IEnumerable<PrestamosadelantoDto>>
{
    public async Task<IEnumerable<PrestamosadelantoDto>> Handle(GetAllPrestamosadelantosQuery request, CancellationToken cancellationToken)
    {
        var prestamos = await unitOfWork.Prestamosadelantos.GetAllAsync(cancellationToken);

        return prestamos.Select(p => new PrestamosadelantoDto(
            p.Id,
            p.TrabajadorId,
            p.Tipo,
            p.MontoTotal,
            p.FechaSolicitud,
            p.Motivo,
            p.NumeroCuotas,
            p.MontoPorCuota,
            p.Estado
        ));
    }
}
