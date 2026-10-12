using System;
using System.Collections.Generic;

namespace PP1_G2.Application.Dtos;

public partial class ClienteDto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public List<string> Telefonos { get; set; } = new();

    public DateTime? FechaNacimiento { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int Activo { get; set; }
}
