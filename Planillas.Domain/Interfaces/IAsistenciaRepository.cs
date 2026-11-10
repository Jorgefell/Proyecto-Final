using Planillas.Domain.Entities;

namespace Planillas.Domain.Interfaces;

/// <summary>
/// Repositorio personalizado de Asistencia. Hereda las operaciones CRUD comunes
/// del repositorio genérico y aquí se agregarían las consultas específicas de la entidad.
/// </summary>
public interface IAsistenciaRepository : IGenericRepository<Asistencia>
{
}
