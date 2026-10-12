using System;
using System.Collections.Generic;

namespace PP1_G2.Domain.Entidades;

public partial class Direcciones
{
    public int IdDireccion { get; set; }

    public string? Distrito { get; set; }

    public string? Canton { get; set; }

    public string? Provincia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
