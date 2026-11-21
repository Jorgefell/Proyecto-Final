using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.Features.Planillas.Commands;
using Planillas.Application.Features.Planillas.Queries;

namespace Planillas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PlanillasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllPlanillasQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPlanillaByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:int}/boletas")]
    public async Task<IActionResult> GetBoletasByPlanilla(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBoletasByPlanillaIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("boletas/{boletaId:int}")]
    public async Task<IActionResult> GetBoletaById(int boletaId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBoletaByIdQuery(boletaId), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("procesar")]
    public async Task<IActionResult> Procesar([FromBody] ProcesarPlanillaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPatch("{id:int}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await mediator.Send(new CambiarEstadoPlanillaCommand(id, request.Estado), cancellationToken);
            return updated ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}

public record CambiarEstadoRequest(string Estado);
