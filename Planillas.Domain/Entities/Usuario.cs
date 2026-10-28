using System;
using System.Collections.Generic;

namespace Planillas.Domain.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public int RolId { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();

    public virtual Role Rol { get; set; } = null!;
}
