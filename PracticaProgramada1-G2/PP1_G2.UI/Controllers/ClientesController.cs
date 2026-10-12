using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PP1_G2.Application.Dtos;
using PP1_G2.Application.Interfaces;

namespace PP1_G2.UI.Controllers;

/// <summary>
/// Expone operaciones HTTP para consultar clientes.
/// </summary>
[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    /// <summary>
    /// Inicializa el controlador con el servicio de clientes.
    /// </summary>
    /// <param name="clienteService">Servicio que obtiene el listado de clientes.</param>
    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Devuelve todos los clientes registrados.
    /// </summary>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Respuesta HTTP 200 con los clientes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClienteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClienteDto>>> ObtenerTodosAsync(CancellationToken cancellationToken)
    {
        var clientes = await _clienteService.ObtenerTodosAsync(cancellationToken);
        return Ok(clientes);
    }

    /// <summary>
    /// Devuelve el detalle de un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador positivo del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Respuesta HTTP 200 con el cliente o 404 si no existe.</returns>
    [HttpGet("{id:int}", Name = "ObtenerClientePorId")]
    [ProducesResponseType(typeof(ClienteDetalleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDetalleDto>> ObtenerPorIdAsync(
        [Range(1, int.MaxValue)] int id,
        CancellationToken cancellationToken)
    {
        var cliente = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    /// <summary>
    /// Registra un cliente nuevo junto con sus teléfonos.
    /// </summary>
    /// <param name="clienteDto">Datos validados del cliente que se va a crear.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Respuesta HTTP 201 con la ubicación y el detalle del cliente.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClienteDetalleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDetalleDto>> CrearAsync(
        [FromBody] CrearClienteDto clienteDto,
        CancellationToken cancellationToken)
    {
        var clienteCreado = await _clienteService.CrearAsync(clienteDto, cancellationToken);
        return CreatedAtRoute("ObtenerClientePorId", new { id = clienteCreado.Id }, clienteCreado);
    }

    /// <summary>
    /// Actualiza los datos generales y los teléfonos de un cliente.
    /// </summary>
    /// <param name="id">Identificador positivo del cliente.</param>
    /// <param name="clienteDto">Datos nuevos del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Respuesta HTTP 204 si se actualizó el cliente o 404 si no existe.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActualizarAsync(
        [Range(1, int.MaxValue)] int id,
        [FromBody] ActualizarClienteDto clienteDto,
        CancellationToken cancellationToken)
    {
        var actualizado = await _clienteService.ActualizarAsync(id, clienteDto, cancellationToken);
        return actualizado ? NoContent() : NotFound();
    }

    /// <summary>
    /// Elimina definitivamente un cliente.
    /// </summary>
    /// <param name="id">Identificador positivo del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Respuesta HTTP 204 si se eliminó el cliente o 404 si no existe.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EliminarAsync(
        [Range(1, int.MaxValue)] int id,
        CancellationToken cancellationToken)
    {
        var eliminado = await _clienteService.EliminarAsync(id, cancellationToken);
        return eliminado ? NoContent() : NotFound();
    }
}
