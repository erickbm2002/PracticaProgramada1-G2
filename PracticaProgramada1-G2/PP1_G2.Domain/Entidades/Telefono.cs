namespace PP1_G2.Domain.Entidades;

/// <summary>
/// Representa un número de teléfono asociado a un cliente.
/// </summary>
public partial class Telefono
{
    public int IdTelefono { get; set; }

    public int ClienteId { get; set; }

    public string Numero { get; set; } = null!;

    public Cliente Cliente { get; set; } = null!;
}
