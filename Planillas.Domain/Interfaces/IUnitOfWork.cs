namespace Planillas.Domain.Interfaces;

/// <summary>
/// Unidad de trabajo. Es el único componente que expone los repositorios
/// y el que confirma los cambios en la base de datos mediante SaveChangesAsync.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    ICargoRepository Cargos { get; }

    IObraRepository Obras { get; }

    ITrabajadorRepository Trabajadores { get; }

    IAsistenciaRepository Asistencias { get; }

    IPrestamosadelantoRepository Prestamosadelantos { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
