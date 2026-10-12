using Microsoft.AspNetCore.Mvc;
using PP1_G2.Application.Dtos;
using PP1_G2.Application.Interfaces;

namespace PP1_G2.UI.Controllers;

/// <summary>
/// Gestiona las páginas web para administrar clientes.
/// </summary>
public sealed class ClientesWebController : Controller
{
    private readonly IClienteService _clienteService;

    /// <summary>
    /// Inicializa el controlador web con el servicio de clientes.
    /// </summary>
    /// <param name="clienteService">Servicio que administra los clientes.</param>
    public ClientesWebController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Muestra el listado de clientes.
    /// </summary>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>La vista con los clientes registrados.</returns>
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var clientes = await _clienteService.ObtenerTodosAsync(cancellationToken);
        return View(clientes);
    }

    /// <summary>
    /// Muestra los datos y teléfonos de un cliente.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>La vista de detalle o 404 si el cliente no existe.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);
        return cliente is null ? NotFound() : View(cliente);
    }

    /// <summary>
    /// Muestra el formulario para crear un cliente.
    /// </summary>
    /// <returns>El formulario de alta.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CrearClienteDto { Telefonos = new List<string> { string.Empty } });
    }

    /// <summary>
    /// Valida y registra un cliente desde el formulario web.
    /// </summary>
    /// <param name="clienteDto">Datos recibidos del formulario.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Redirección al listado o el formulario con errores.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearClienteDto clienteDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(clienteDto);
        }

        await _clienteService.CrearAsync(clienteDto, cancellationToken);
        TempData["Mensaje"] = "El cliente se registró correctamente.";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Muestra el formulario de edición con los datos actuales.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>El formulario de edición o 404 si el cliente no existe.</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);
        if (cliente is null)
        {
            return NotFound();
        }

        var modelo = new ActualizarClienteDto
        {
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Email = cliente.Email,
            FechaNacimiento = cliente.FechaNacimiento,
            Ciudad = cliente.Ciudad,
            Pais = cliente.Pais,
            Telefonos = cliente.Telefonos.Count > 0
                ? cliente.Telefonos.ToList()
                : new List<string> { string.Empty }
        };

        ViewBag.Id = id;
        return View(modelo);
    }

    /// <summary>
    /// Valida y actualiza un cliente desde el formulario web.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="clienteDto">Datos recibidos del formulario.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Redirección al listado o el formulario con errores.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ActualizarClienteDto clienteDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Id = id;
            return View(clienteDto);
        }

        var actualizado = await _clienteService.ActualizarAsync(id, clienteDto, cancellationToken);
        if (!actualizado)
        {
            return NotFound();
        }

        TempData["Mensaje"] = "El cliente se actualizó correctamente.";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Muestra la confirmación de borrado de un cliente.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>La confirmación o 404 si el cliente no existe.</returns>
    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);
        return cliente is null ? NotFound() : View(cliente);
    }

    /// <summary>
    /// Elimina el cliente confirmado desde la interfaz web.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token que permite cancelar la solicitud.</param>
    /// <returns>Redirección al listado o 404 si el cliente no existe.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var eliminado = await _clienteService.EliminarAsync(id, cancellationToken);
        if (!eliminado)
        {
            return NotFound();
        }

        TempData["Mensaje"] = "El cliente y sus teléfonos se eliminaron correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
