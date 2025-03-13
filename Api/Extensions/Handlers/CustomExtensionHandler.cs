using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions.Handlers;

public class CustomExtensionHandler(
    ILogger<CustomExtensionHandler> logger)
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
            NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status404NotFound
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