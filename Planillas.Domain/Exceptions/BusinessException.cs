namespace Planillas.Domain.Exceptions;

/// <summary>
/// Excepción base para errores de negocio. El ExceptionMiddleware de la API
/// la captura y la traduce a una respuesta HTTP 400 Bad Request.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException()
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
