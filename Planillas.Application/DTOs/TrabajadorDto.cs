namespace Planillas.Application.DTOs;

public class TrabajadorDto
{
    public int Id { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string TipoContrato { get; set; } = null!;
    public string ModalidadPago { get; set; } = null!;
    public decimal RemuneracionBase { get; set; }
    public int CargoId { get; set; }
    public string? CargoNombre { get; set; }
    public int? ObraActualId { get; set; }
    public DateTime FechaIngreso { get; set; }
    public string? EstadoLaboral { get; set; }
}
