namespace Planillas.Application.DTOs;

public record CargoDto(
    int Id,
    string Nombre,
    string? Area,
    decimal? SueldoBaseReferencial
);