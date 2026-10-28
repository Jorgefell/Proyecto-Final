using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.Features.Trabajadores.Commands;

namespace Planillas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrabajadoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrabajadoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTrabajadorCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id, message = "Trabajador registrado exitosamente" });
    }
}