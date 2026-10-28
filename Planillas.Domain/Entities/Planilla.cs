using System;
using System.Collections.Generic;

namespace Planillas.Infrastructure;

public partial class Planilla
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public int? ObraId { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string? Estado { get; set; }

    public int UsuarioProcesoId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Boletaspago> Boletaspagos { get; set; } = new List<Boletaspago>();

    public virtual Obra? Obra { get; set; }

    public virtual Usuario UsuarioProceso { get; set; } = null!;
}
