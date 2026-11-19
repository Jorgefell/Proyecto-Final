using MediatR;
using Planillas.Application.DTOs;
using Planillas.Application.Interfaces;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<AuthResponseDto>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.Usuarios.GetByUsernameWithRoleAsync(request.Username);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var rolNombre = usuario.Rol.Nombre;
        var (token, expiracion) = _jwtService.GenerateToken(usuario, rolNombre);

        return new AuthResponseDto(token, usuario.Username, rolNombre, expiracion);
    }
}