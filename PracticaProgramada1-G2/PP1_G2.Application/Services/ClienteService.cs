using AutoMapper;
using PP1_G2.Application.Dtos;
using PP1_G2.Application.Interfaces;
using PP1_G2.Domain.Entidades;

namespace PP1_G2.Application.Services;

/// <summary>
/// Implementa las operaciones de aplicación relacionadas con clientes.
/// </summary>
public sealed class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa el servicio con el repositorio y el mapeador requeridos.
    /// </summary>
    /// <param name="clienteRepository">Repositorio que proporciona los clientes.</param>
    /// <param name="mapper">Mapeador de entidades a DTO.</param>
    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        var clientes = await _clienteRepository.ObtenerTodosAsync(cancellationToken);
        return _mapper.Map<List<ClienteDto>>(clientes);
    }

    /// <inheritdoc />
    public async Task<ClienteDetalleDto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return null;
        }

        var cliente = await _clienteRepository.ObtenerPorIdAsync(id, cancellationToken);
        return cliente is null ? null : _mapper.Map<ClienteDetalleDto>(cliente);
    }

    /// <inheritdoc />
    public async Task<ClienteDetalleDto> CrearAsync(CrearClienteDto clienteDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(clienteDto);

        if (string.IsNullOrWhiteSpace(clienteDto.Nombre))
        {
            throw new ArgumentException("El nombre del cliente es obligatorio.", nameof(clienteDto));
        }

        if (clienteDto.Telefonos is null || clienteDto.Telefonos.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("La lista de teléfonos no puede ser nula ni contener valores vacíos.", nameof(clienteDto));
        }

        var cliente = new Cliente
        {
            Nombre = clienteDto.Nombre.Trim(),
            Apellido = clienteDto.Apellido?.Trim(),
            Email = clienteDto.Email?.Trim(),
            FechaNacimiento = clienteDto.FechaNacimiento,
            Ciudad = clienteDto.Ciudad?.Trim(),
            Pais = clienteDto.Pais?.Trim(),
            FechaCreacion = DateTime.UtcNow,
            Activo = 1,
            Telefonos = clienteDto.Telefonos
                .Select(numero => new Telefono { Numero = numero.Trim() })
                .ToList()
        };

        await _clienteRepository.AgregarAsync(cliente, cancellationToken);
        return _mapper.Map<ClienteDetalleDto>(cliente);
    }

    /// <inheritdoc />
    public async Task<bool> ActualizarAsync(int id, ActualizarClienteDto clienteDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(clienteDto);

        if (id <= 0 || string.IsNullOrWhiteSpace(clienteDto.Nombre)
            || clienteDto.Telefonos is null
            || clienteDto.Telefonos.Any(string.IsNullOrWhiteSpace))
        {
            return false;
        }

        var cliente = await _clienteRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (cliente is null)
        {
            return false;
        }

        cliente.Nombre = clienteDto.Nombre.Trim();
        cliente.Apellido = clienteDto.Apellido?.Trim();
        cliente.Email = clienteDto.Email?.Trim();
        cliente.FechaNacimiento = clienteDto.FechaNacimiento;
        cliente.Ciudad = clienteDto.Ciudad?.Trim();
        cliente.Pais = clienteDto.Pais?.Trim();
        cliente.FechaModificacion = DateTime.UtcNow;
        cliente.Telefonos.Clear();

        foreach (var numero in clienteDto.Telefonos)
        {
            cliente.Telefonos.Add(new Telefono { Numero = numero.Trim() });
        }

        await _clienteRepository.GuardarCambiosAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            return false;
        }

        var cliente = await _clienteRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (cliente is null)
        {
            return false;
        }

        await _clienteRepository.EliminarAsync(cliente, cancellationToken);
        return true;
    }
}
