using Microsoft.EntityFrameworkCore;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;
using Planillas.Infrastructure.Persistence;

namespace Planillas.Infrastructure.Repositories;

/// <summary>
/// Repositorio concreto de Trabajador. Implementa las consultas o métodos
/// específicos de la entidad y delega las operaciones comunes al repositorio genérico.
/// </summary>
public class TrabajadorRepository(ApplicationDbContext context)
    : GenericRepository<Trabajadore>(context), ITrabajadorRepository
{
}
