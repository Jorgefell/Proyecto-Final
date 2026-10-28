using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Asistencia
{
    public int Id { get; set; }

    public int TrabajadorId { get; set; }

    public int ObraId { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public decimal? HorasTrabajadas { get; set; }

    public decimal? HorasExtras { get; set; }

    public int? MinutosTardanza { get; set; }

    public string? Observaciones { get; set; }

    public virtual Obra Obra { get; set; } = null!;

    public virtual Trabajadore Trabajador { get; set; } = null!;
}
