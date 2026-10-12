using PP1_G2.Application.Dtos;

namespace PP1_G2.Application.Interfaces;

/// <summary>
/// Define las operaciones de aplicación relacionadas con clientes.
/// </summary>
public interface IClienteService
{
    /// <summary>
    /// Obtiene todos los clientes con sus datos de respuesta.
    /// </summary>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>Una colección de clientes DTO.</returns>
    Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene los datos completos de un cliente.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>El detalle del cliente o null si no existe.</returns>
    Task<ClienteDetalleDto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registra un cliente y devuelve sus datos persistidos.
    /// </summary>
    /// <param name="clienteDto">Datos del cliente que se va a crear.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>El detalle del cliente creado.</returns>
    Task<ClienteDetalleDto> CrearAsync(CrearClienteDto clienteDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza los datos generales y los teléfonos de un cliente existente.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="clienteDto">Datos nuevos del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>True si se actualizó el cliente; false si no existe o el identificador no es válido.</returns>
    Task<bool> ActualizarAsync(int id, ActualizarClienteDto clienteDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina definitivamente un cliente existente.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>True si se eliminó el cliente; false si no existe o el identificador no es válido.</returns>
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
