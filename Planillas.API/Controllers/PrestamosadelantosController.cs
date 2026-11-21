using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.Features.Prestamosadelantos.Commands;
using Planillas.Application.Features.Prestamosadelantos.Queries;

namespace Planillas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrestamosadelantosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllPrestamosadelantosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPrestamosadelantoByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrestamosadelantoCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePrestamosadelantoCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id) return BadRequest();
        var updated = await mediator.Send(command, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeletePrestamosadelantoCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
