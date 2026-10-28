using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Prestamosadelanto
{
    public int Id { get; set; }

    public int TrabajadorId { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal MontoTotal { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public string? Motivo { get; set; }

    public int? NumeroCuotas { get; set; }

    public decimal MontoPorCuota { get; set; }

    public string? Estado { get; set; }

    public virtual Trabajadore Trabajador { get; set; } = null!;
}
