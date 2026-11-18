using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

/// <summary>
/// Implementación de la Unidad de Trabajo. Es el único componente que accede a los
/// repositorios y centraliza la confirmación de cambios con SaveChangesAsync.
/// </summary>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public ICargoRepository Cargos { get; } = new CargoRepository(context);

    public IObraRepository Obras { get; } = new ObraRepository(context);

    public ITrabajadorRepository Trabajadores { get; } = new TrabajadorRepository(context);

    public IAsistenciaRepository Asistencias { get; } = new AsistenciaRepository(context);

    public IPrestamosadelantoRepository Prestamosadelantos { get; } = new PrestamosadelantoRepository(context);

    public IPlanillaRepository Planillas { get; } = new PlanillaRepository(context);

    public IBoletaspagoRepository Boletaspagos { get; } = new BoletaspagoRepository(context);

    public IDetallesboletumRepository Detallesboleta { get; } = new DetallesboletumRepository(context);

    public IUsuarioRepository Usuarios { get; } = new UsuarioRepository(context);

    public IRoleRepository Roles { get; } = new RoleRepository(context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
