namespace Planillas.Application.DTOs;

public record DetallesboletumDto(
    int Id,
    int BoletaPagoId,
    string Tipo,
    string Concepto,
    decimal Monto
);
