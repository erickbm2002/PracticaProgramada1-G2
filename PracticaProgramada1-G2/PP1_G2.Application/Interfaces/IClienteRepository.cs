using PP1_G2.Domain.Entidades;

namespace PP1_G2.Application.Interfaces;

/// <summary>
/// Define las operaciones de consulta disponibles para los clientes.
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Recupera todos los clientes registrados.
    /// </summary>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>Una colección de clientes.</returns>
    Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera un cliente y sus teléfonos mediante su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>El cliente encontrado o null si no existe.</returns>
    Task<Cliente?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persiste un cliente nuevo y sus teléfonos.
    /// </summary>
    /// <param name="cliente">Cliente que se va a agregar.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Recupera un cliente y sus teléfonos con seguimiento para modificarlos.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    /// <returns>El cliente rastreado o null si no existe.</returns>
    Task<Cliente?> ObtenerParaActualizarAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Guarda los cambios pendientes del contexto.
    /// </summary>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    Task GuardarCambiosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un cliente y persiste el borrado.
    /// </summary>
    /// <param name="cliente">Cliente que se va a eliminar.</param>
    /// <param name="cancellationToken">Token que permite cancelar la operación.</param>
    Task EliminarAsync(Cliente cliente, CancellationToken cancellationToken = default);
}
