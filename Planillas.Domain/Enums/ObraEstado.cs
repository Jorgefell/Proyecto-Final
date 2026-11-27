using Planillas.Domain.Exceptions;

namespace Planillas.Domain.Enums;

public static class ObraEstado
{
    public const string Planificacion = "Planificacion";
    public const string EnEjecucion = "En Ejecucion";
    public const string Suspendida = "Suspendida";
    public const string Finalizada = "Finalizada";

    private static readonly string[] EstadosPermitidos =
    [
        Planificacion,
        EnEjecucion,
        Suspendida,
        Finalizada
    ];

    public static string? Normalizar(string? estado)
    {
        if (estado is null)
        {
            return null;
        }

        var valor = estado.Trim();

        if (string.Equals(valor, "Activa", StringComparison.OrdinalIgnoreCase))
        {
            return EnEjecucion;
        }

        foreach (var estadoPermitido in EstadosPermitidos)
        {
            if (string.Equals(valor, estadoPermitido, StringComparison.OrdinalIgnoreCase))
            {
                return estadoPermitido;
            }
        }

        throw new BusinessException(
            $"Estado de obra no válido. Valores permitidos: {string.Join(", ", EstadosPermitidos)}.");
    }
}
