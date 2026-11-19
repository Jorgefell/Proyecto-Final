using Planillas.Domain.Entities;

namespace Planillas.Application.Interfaces;

public interface IJwtService
{
    (string Token, DateTime Expiracion) GenerateToken(Usuario usuario, string rolNombre);
}