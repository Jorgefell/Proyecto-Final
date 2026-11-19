namespace Planillas.Application.DTOs;

public record LoginRequestDto(string Username, string Password);

public record RegisterRequestDto(string Username, string Password, int IdRol);

public record AuthResponseDto(string Token, string Username, string Rol, DateTime Expiracion);