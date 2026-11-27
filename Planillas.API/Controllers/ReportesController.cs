using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planillas.Application.Interfaces;

namespace Planillas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet("costos-por-obra")]
    [Authorize(Roles = "Admin,Administrador,Contador")]
    public async Task<IActionResult> GetCostosPorObra([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin)
    {
        var reporte = await _reporteService.GetCostosPorObraAsync(fechaInicio, fechaFin);
        return Ok(reporte);
    }

    [HttpGet("resumen-planilla/{planillaId:int}")]
    [Authorize(Roles = "Admin,Administrador,Contador,RRHH")]
    public async Task<IActionResult> GetResumenPlanilla(int planillaId)
    {
        var resumen = await _reporteService.GetResumenPlanillaAsync(planillaId);
        if (resumen == null) return NotFound("Planilla no encontrada.");
        return Ok(resumen);
    }

    [HttpGet("boleta/{boletaId:int}")]
    public async Task<IActionResult> GetBoletaIndividual(int boletaId)
    {
        var boleta = await _reporteService.GetBoletaIndividualAsync(boletaId);
        if (boleta == null) return NotFound("Boleta no encontrada.");
        return Ok(boleta);
    }
}