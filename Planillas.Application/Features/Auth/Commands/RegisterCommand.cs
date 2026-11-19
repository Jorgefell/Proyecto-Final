using MediatR;
using Planillas.Application.DTOs;
using Planillas.Application.Interfaces;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Auth.Commands;

public record RegisterCommand(string Username, string Password, int IdRol) : IRequest<AuthResponseDto>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Usuarios.ExistsByUsernameAsync(request.Username))
            throw new InvalidOperationException("El nombre de usuario ya existe.");

        var rol = await _unitOfWork.Roles.GetByIdAsync(request.IdRol, cancellationToken)
                  ?? throw new KeyNotFoundException("El rol especificado no existe.");

        var usuario = new Usuario
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            NombreCompleto = request.Username,
            RolId = request.IdRol,
            Activo = true
        };

        await _unitOfWork.Usuarios.AddAsync(usuario, cancellationToken);

        var (token, expiracion) = _jwtService.GenerateToken(usuario, rol.Nombre);
        return new AuthResponseDto(token, usuario.Username, rol.Nombre, expiracion);
    }
}