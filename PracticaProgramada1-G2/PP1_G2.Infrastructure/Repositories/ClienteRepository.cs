using Microsoft.EntityFrameworkCore;
using PP1_G2.Application.Interfaces;
using PP1_G2.Domain.Entidades;
using PP1_G2.Infrastructure.Data;

namespace PP1_G2.Infrastructure.Repositories;

/// <summary>
/// Implementa las consultas de clientes mediante Entity Framework Core.
/// </summary>
public sealed class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Inicializa el repositorio con el contexto de la aplicación.
    /// </summary>
    /// <param name="dbContext">Contexto usado para consultar la base de datos.</param>
    public ClienteRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<Cliente> consulta = _dbContext.Clientes
            .AsNoTracking()
            .Include(cliente => cliente.Telefonos);
        return await consulta.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Cliente?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Clientes
            .AsNoTracking()
            .Include(cliente => cliente.Telefonos)
            .FirstOrDefaultAsync(cliente => cliente.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        await _dbContext.Clientes.AddAsync(cliente, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Cliente?> ObtenerParaActualizarAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Clientes
            .AsTracking()
            .Include(cliente => cliente.Telefonos)
            .FirstOrDefaultAsync(cliente => cliente.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task GuardarCambiosAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task EliminarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        _dbContext.Clientes.Remove(cliente);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
