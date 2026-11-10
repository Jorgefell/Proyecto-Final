namespace Planillas.Application.DTOs;

public record TrabajadorDto(
    int Id,
    string? TipoDocumento,
    string NumeroDocumento,
    string Nombres,
    string Apellidos,
    string? Telefono,
    int CargoId,
    int? ObraActualId,
    DateTime FechaIngreso,
    DateTime? FechaCese,
    string TipoContrato,
    string ModalidadPago,
    decimal RemuneracionBase,
    string? EstadoLaboral
);