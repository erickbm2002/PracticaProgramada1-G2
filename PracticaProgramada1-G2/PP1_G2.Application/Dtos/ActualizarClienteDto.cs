using System.ComponentModel.DataAnnotations;

namespace PP1_G2.Application.Dtos;

/// <summary>
/// Contiene los datos necesarios para actualizar un cliente y sus teléfonos.
/// </summary>
public sealed class ActualizarClienteDto : IValidatableObject
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Apellido { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public List<string> Telefonos { get; set; } = new();

    /// <summary>
    /// Valida que los teléfonos incluidos no estén vacíos.
    /// </summary>
    /// <param name="validationContext">Contexto usado por el validador.</param>
    /// <returns>Los errores de validación encontrados.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Telefonos is null)
        {
            yield return new ValidationResult("La lista de teléfonos no puede ser nula.", [nameof(Telefonos)]);
            yield break;
        }

        if (Telefonos.Any(string.IsNullOrWhiteSpace))
        {
            yield return new ValidationResult("Los teléfonos no pueden estar vacíos.", [nameof(Telefonos)]);
        }
    }
}
