using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Trabajadore
{
    public int Id { get; set; }

    public string? TipoDocumento { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public DateTime FechaIngreso { get; set; }

    public DateTime? FechaCese { get; set; }

    public string TipoContrato { get; set; } = null!;

    public string ModalidadPago { get; set; } = null!;

    public decimal RemuneracionBase { get; set; }

    public int CargoId { get; set; }

    public int? ObraActualId { get; set; }

    public string? EstadoLaboral { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Boletaspago> Boletaspagos { get; set; } = new List<Boletaspago>();

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Obra? ObraActual { get; set; }

    public virtual ICollection<Prestamosadelanto> Prestamosadelantos { get; set; } = new List<Prestamosadelanto>();
}
