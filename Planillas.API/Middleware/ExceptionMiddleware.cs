using System.Net;
using System.Text.Json;
using Planillas.API.Models;
using Planillas.Domain.Exceptions;

namespace Planillas.API.Middleware;

/// <summary>
/// Middleware global de manejo de excepciones. Captura las excepciones de negocio
/// y devuelve respuestas JSON limpias con el código HTTP adecuado, en lugar de
/// errores 500 sin formato.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                BusinessException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new ErrorResponse(context.Response.StatusCode, ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
