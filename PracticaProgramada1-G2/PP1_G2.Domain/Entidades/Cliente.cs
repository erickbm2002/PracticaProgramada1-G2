using System;
using System.Collections.Generic;

namespace PP1_G2.Domain.Entidades;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();

    public DateTime? FechaNacimiento { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int Activo { get; set; }
}
