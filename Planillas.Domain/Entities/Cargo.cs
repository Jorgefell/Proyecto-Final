using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Cargo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Area { get; set; }

    public decimal? SueldoBaseReferencial { get; set; }

    public virtual ICollection<Trabajadore> Trabajadores { get; set; } = new List<Trabajadore>();
}
