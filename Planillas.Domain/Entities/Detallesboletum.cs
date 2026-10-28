using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Detallesboletum
{
    public int Id { get; set; }

    public int BoletaPagoId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public virtual Boletaspago BoletaPago { get; set; } = null!;
}
