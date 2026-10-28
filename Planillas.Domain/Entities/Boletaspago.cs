using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Boletaspago
{
    public int Id { get; set; }

    public int PlanillaId { get; set; }

    public int TrabajadorId { get; set; }

    public int DiasLaborados { get; set; }

    public int? DiasFalta { get; set; }

    public decimal? HorasExtrasTotales { get; set; }

    public decimal RemuneracionBasica { get; set; }

    public decimal TotalIngresos { get; set; }

    public decimal TotalDescuentos { get; set; }

    public decimal? TotalAportesEmpresa { get; set; }

    public decimal NetoPagar { get; set; }

    public virtual ICollection<Detallesboletum> Detallesboleta { get; set; } = new List<Detallesboletum>();

    public virtual Planilla Planilla { get; set; } = null!;

    public virtual Trabajadore Trabajador { get; set; } = null!;
}
