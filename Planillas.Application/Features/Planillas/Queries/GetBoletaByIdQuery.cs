using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Queries;

public record GetBoletaByIdQuery(int Id) : IRequest<BoletaspagoDto?>;

public class GetBoletaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetBoletaByIdQuery, BoletaspagoDto?>
{
    public async Task<BoletaspagoDto?> Handle(GetBoletaByIdQuery request, CancellationToken cancellationToken)
    {
        var boleta = await unitOfWork.Boletaspagos.GetByIdWithDetallesAsync(request.Id, cancellationToken);
        if (boleta is null) return null;

        return new BoletaspagoDto(
            boleta.Id,
            boleta.PlanillaId,
            boleta.TrabajadorId,
            $"{boleta.Trabajador.Nombres} {boleta.Trabajador.Apellidos}",
            boleta.Trabajador.NumeroDocumento,
            boleta.Trabajador.Cargo?.Nombre,
            boleta.DiasLaborados,
            boleta.DiasFalta,
            boleta.HorasExtrasTotales,
            boleta.RemuneracionBasica,
            boleta.TotalIngresos,
            boleta.TotalDescuentos,
            boleta.TotalAportesEmpresa,
            boleta.NetoPagar,
            boleta.Detallesboleta.Select(d => new DetallesboletumDto(
                d.Id,
                d.BoletaPagoId,
                d.Tipo,
                d.Concepto,
                d.Monto
            )).ToList()
        );
    }
}
