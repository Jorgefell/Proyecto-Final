namespace Planillas.API.Models;

/// <summary>
/// Modelo de respuesta estandarizado para errores controlados.
/// Lo devuelve el ExceptionMiddleware en formato JSON.
/// </summary>
public record ErrorResponse(int Status, string Message);
