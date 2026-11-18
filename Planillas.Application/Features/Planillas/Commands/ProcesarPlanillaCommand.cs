using MediatR;
using Planillas.Application.DTOs;
using Planillas.Domain.Entities;
using Planillas.Domain.Interfaces;

namespace Planillas.Application.Features.Planillas.Commands;

public record ProcesarPlanillaCommand(
    string? Codigo,
    int? ObraId,
    string Tipo,
    DateTime FechaInicio,
    DateTime FechaFin,
    int UsuarioProcesoId
) : IRequest<PlanillaDetalleDto>;

public class ProcesarPlanillaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ProcesarPlanillaCommand, PlanillaDetalleDto>
{
    public async Task<PlanillaDetalleDto> Handle(ProcesarPlanillaCommand request, CancellationToken cancellationToken)
    {
        if (request.FechaFin.Date < request.FechaInicio.Date)
            throw new InvalidOperationException("La fecha de fin no puede ser menor que la fecha de inicio.");

        var tipoNormalizado = string.IsNullOrWhiteSpace(request.Tipo)
            ? "MENSUAL"
            : request.Tipo.Trim().ToUpperInvariant();

        var existeSolapamiento = await unitOfWork.Planillas.ExistePlanillaEnPeriodoAsync(
            request.ObraId,
            tipoNormalizado,
            request.FechaInicio.Date,
            request.FechaFin.Date,
            cancellationToken);

        if (existeSolapamiento)
            throw new InvalidOperationException("Ya existe una planilla activa que se cruza con el periodo seleccionado.");

        var codigo = string.IsNullOrWhiteSpace(request.Codigo)
            ? $"PLN-{request.FechaInicio:yyyyMM}-{Random.Shared.Next(100, 999)}"
            : request.Codigo.Trim().ToUpperInvariant();

        if (await unitOfWork.Planillas.ExisteCodigoAsync(codigo, cancellationToken))
            throw new InvalidOperationException($"Ya existe una planilla con el código '{codigo}'.");

        Obra? obra = null;
        if (request.ObraId.HasValue)
        {
            obra = await unitOfWork.Obras.GetByIdAsync(request.ObraId.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"No se encontró la obra con ID {request.ObraId.Value}.");
        }

        var todosTrabajadores = await unitOfWork.Trabajadores.GetAllAsync(cancellationToken);
        var trabajadoresFiltrados = todosTrabajadores
            .Where(t => (string.IsNullOrWhiteSpace(t.EstadoLaboral) || t.EstadoLaboral.Equals("ACTIVO", StringComparison.OrdinalIgnoreCase))
                        && t.FechaIngreso.Date <= request.FechaFin.Date
                        && (!t.FechaCese.HasValue || t.FechaCese.Value.Date >= request.FechaInicio.Date)
                        && (!request.ObraId.HasValue || t.ObraActualId == request.ObraId.Value))
            .ToList();

        if (trabajadoresFiltrados.Count == 0)
            throw new InvalidOperationException("No hay trabajadores activos para procesar en el periodo u obra indicados.");

        var cargos = (await unitOfWork.Cargos.GetAllAsync(cancellationToken))
            .ToDictionary(c => c.Id, c => c.Nombre);

        var asistenciasPeriodo = (await unitOfWork.Asistencias.GetAllAsync(cancellationToken))
            .Where(a => a.Fecha.Date >= request.FechaInicio.Date
                        && a.Fecha.Date <= request.FechaFin.Date
                        && (!request.ObraId.HasValue || a.ObraId == request.ObraId.Value))
            .ToList();

        var prestamosActivos = (await unitOfWork.Prestamosadelantos.GetAllAsync(cancellationToken))
            .Where(p => p.FechaSolicitud.Date <= request.FechaFin.Date
                        && (string.IsNullOrWhiteSpace(p.Estado)
                            || p.Estado.Equals("APROBADO", StringComparison.OrdinalIgnoreCase)
                            || p.Estado.Equals("ACTIVO", StringComparison.OrdinalIgnoreCase)
                            || p.Estado.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase)))
            .ToList();

        int diasBasePeriodo = tipoNormalizado switch
        {
            "SEMANAL" => 7,
            "QUINCENAL" => 15,
            _ => Math.Min(30, (request.FechaFin.Date - request.FechaInicio.Date).Days + 1)
        };
        if (diasBasePeriodo <= 0) diasBasePeriodo = 30;

        var planilla = new Planilla
        {
            Codigo = codigo,
            ObraId = request.ObraId,
            Tipo = tipoNormalizado,
            FechaInicio = request.FechaInicio.Date,
            FechaFin = request.FechaFin.Date,
            Estado = "PROCESADA",
            UsuarioProcesoId = request.UsuarioProcesoId,
            FechaCreacion = DateTime.Now
        };

        var boletasDto = new List<BoletaspagoDto>();

        foreach (var trabajador in trabajadoresFiltrados)
        {
            var asistenciasTrabajador = asistenciasPeriodo
                .Where(a => a.TrabajadorId == trabajador.Id)
                .ToList();

            int diasFalta = asistenciasTrabajador.Count(a =>
                a.Estado.Contains("FALTA", StringComparison.OrdinalIgnoreCase) ||
                a.Estado.Contains("INASIST", StringComparison.OrdinalIgnoreCase));

            int diasLaborados = Math.Max(0, diasBasePeriodo - diasFalta);
            decimal horasExtrasTotales = asistenciasTrabajador.Sum(a => a.HorasExtras ?? 0m);
            int minutosTardanza = asistenciasTrabajador.Sum(a => a.MinutosTardanza ?? 0);

            bool esPagoDiario = !string.IsNullOrWhiteSpace(trabajador.ModalidadPago) &&
                                (trabajador.ModalidadPago.Contains("JORNAL", StringComparison.OrdinalIgnoreCase) ||
                                 trabajador.ModalidadPago.Contains("DIARIO", StringComparison.OrdinalIgnoreCase));

            decimal valorDia = esPagoDiario
                ? trabajador.RemuneracionBase
                : trabajador.RemuneracionBase / 30m;
            decimal valorHora = valorDia / 8m;

            decimal remuneracionBasica = Math.Round(valorDia * diasLaborados, 2);
            decimal montoHorasExtras = Math.Round(horasExtrasTotales * valorHora * 1.25m, 2);
            decimal totalIngresos = remuneracionBasica + montoHorasExtras;

            decimal descuentoTardanzas = Math.Round((minutosTardanza / 60m) * valorHora, 2);
            decimal descuentoPension = Math.Round(totalIngresos * 0.13m, 2);
            decimal descuentoPrestamos = Math.Round(
                prestamosActivos
                    .Where(p => p.TrabajadorId == trabajador.Id)
                    .Sum(p => Math.Min(p.MontoPorCuota, p.MontoTotal)), 2);

            decimal totalDescuentos = descuentoTardanzas + descuentoPension + descuentoPrestamos;
            decimal aporteEssalud = Math.Round(totalIngresos * 0.09m, 2);
            decimal netoPagar = Math.Max(0m, Math.Round(totalIngresos - totalDescuentos, 2));

            var boleta = new Boletaspago
            {
                TrabajadorId = trabajador.Id,
                DiasLaborados = diasLaborados,
                DiasFalta = diasFalta,
                HorasExtrasTotales = horasExtrasTotales,
                RemuneracionBasica = remuneracionBasica,
                TotalIngresos = totalIngresos,
                TotalDescuentos = totalDescuentos,
                TotalAportesEmpresa = aporteEssalud,
                NetoPagar = netoPagar
            };

            boleta.Detallesboleta.Add(new Detallesboletum
            {
                Tipo = "INGRESO",
                Concepto = "Remuneración Básica",
                Monto = remuneracionBasica
            });

            if (montoHorasExtras > 0)
            {
                boleta.Detallesboleta.Add(new Detallesboletum
                {
                    Tipo = "INGRESO",
                    Concepto = "Horas Extras (25%)",
                    Monto = montoHorasExtras
                });
            }

            if (descuentoPension > 0)
            {
                boleta.Detallesboleta.Add(new Detallesboletum
                {
                    Tipo = "DESCUENTO",
                    Concepto = "Aporte Obligatorio Pensión (13%)",
                    Monto = descuentoPension
                });
            }

            if (descuentoTardanzas > 0)
            {
                boleta.Detallesboleta.Add(new Detallesboletum
                {
                    Tipo = "DESCUENTO",
                    Concepto = "Descuento por Tardanzas",
                    Monto = descuentoTardanzas
                });
            }

            if (descuentoPrestamos > 0)
            {
                boleta.Detallesboleta.Add(new Detallesboletum
                {
                    Tipo = "DESCUENTO",
                    Concepto = "Cuota Préstamo / Adelanto",
                    Monto = descuentoPrestamos
                });
            }

            if (aporteEssalud > 0)
            {
                boleta.Detallesboleta.Add(new Detallesboletum
                {
                    Tipo = "APORTE",
                    Concepto = "EsSalud Empleador (9%)",
                    Monto = aporteEssalud
                });
            }

            planilla.Boletaspagos.Add(boleta);

            cargos.TryGetValue(trabajador.CargoId, out var cargoNombre);
            boletasDto.Add(new BoletaspagoDto(
                0,
                0,
                trabajador.Id,
                $"{trabajador.Nombres} {trabajador.Apellidos}",
                trabajador.NumeroDocumento,
                cargoNombre,
                diasLaborados,
                diasFalta,
                horasExtrasTotales,
                remuneracionBasica,
                totalIngresos,
                totalDescuentos,
                aporteEssalud,
                netoPagar,
                boleta.Detallesboleta.Select(d => new DetallesboletumDto(0, 0, d.Tipo, d.Concepto, d.Monto)).ToList()
            ));
        }

        await unitOfWork.Planillas.AddAsync(planilla, cancellationToken);

        var boletasFinales = planilla.Boletaspagos.Zip(boletasDto, (b, dto) => dto with
        {
            Id = b.Id,
            PlanillaId = planilla.Id,
            Detalles = b.Detallesboleta.Select(d => new DetallesboletumDto(d.Id, b.Id, d.Tipo, d.Concepto, d.Monto)).ToList()
        }).ToList();

        return new PlanillaDetalleDto(
            planilla.Id,
            planilla.Codigo,
            planilla.ObraId,
            obra?.Nombre,
            planilla.Tipo,
            planilla.FechaInicio,
            planilla.FechaFin,
            planilla.Estado,
            planilla.UsuarioProcesoId,
            planilla.FechaCreacion,
            boletasFinales.Sum(b => b.TotalIngresos),
            boletasFinales.Sum(b => b.TotalDescuentos),
            boletasFinales.Sum(b => b.TotalAportesEmpresa ?? 0m),
            boletasFinales.Sum(b => b.NetoPagar),
            boletasFinales
        );
    }
}
