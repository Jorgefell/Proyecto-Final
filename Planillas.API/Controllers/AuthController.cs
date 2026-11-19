using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.DTOs;
using Planillas.Application.Features.Auth.Commands;

namespace Planillas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto.Username, dto.Password));
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto dto)
    {
        var result = await _mediator.Send(new RegisterCommand(dto.Username, dto.Password, dto.IdRol));
        return Ok(result);
    }
}