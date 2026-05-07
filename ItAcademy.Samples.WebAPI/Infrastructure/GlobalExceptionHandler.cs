using Microsoft.AspNetCore.Diagnostics;
using SampleSolution.Core.Exceptions;

namespace ItAcademy.Samples.WebAPI.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = MapException(exception);

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "An unhandled exception occurred: {Message}.", exception.Message);
        }
        else
        {
            logger.LogWarning(exception, "A handled exception occurred: {Message}.", exception.Message);
        }

        var problemDetails = ApiProblemDetailsFactory.Create(httpContext, statusCode, title, exception.Message, exception);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static (int, string) MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Requested resource was not found."),
            BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request."),
            AlreadyExistsException => (StatusCodes.Status409Conflict, "Resource already exists."),
            InternalServerErrorException => (StatusCodes.Status500InternalServerError, "Internal server error."),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error.")

        };
    }
}