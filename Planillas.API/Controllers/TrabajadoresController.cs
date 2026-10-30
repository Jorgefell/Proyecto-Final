using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.Features.Trabajadores.Commands;
using Planillas.Application.Features.Trabajadores.Queries;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trabajador = await _mediator.Send(new GetTrabajadorByIdQuery(id));
        if (trabajador is null)
            return NotFound(new { message = $"No se encontro el trabajador con Id {id}" });

        return Ok(trabajador);
    }
}