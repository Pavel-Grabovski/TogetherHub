using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public class ExtensionHandler(
    ILogger<ExtensionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogWarning(
            $"Handled exception: {exception.Message}, time:{DateTime.UtcNow}");

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            ExceptionBase exceptionBase => (
                exceptionBase.Message,
                exceptionBase.GetType().Name,
                httpContext.Response.StatusCode = exceptionBase.StatusCode
            ),

            _ => (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
        };

        ProblemDetails problemDetails = new()
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        await httpContext
            .Response
            .WriteAsJsonAsync( problemDetails, cancellationToken );

        return true;
    }
}