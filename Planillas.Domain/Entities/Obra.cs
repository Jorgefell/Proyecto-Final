using System;
using System.Collections.Generic;

namespace Planillas.Infrastructure;

public partial class Obra
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFinEstimada { get; set; }

    public string? Estado { get; set; }

    public decimal? PresupuestoManoObra { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();

    public virtual ICollection<Trabajadore> Trabajadores { get; set; } = new List<Trabajadore>();
}
