using System.Text.Json;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.WebApi.Models;

namespace PersonsDirectory.WebApi.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate requestDelegate,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception ex)
    {
        var (status, message, errors) = ex switch
        {
            ValidationException v => (StatusCodes.Status400BadRequest, v.Message, v.Errors),
            NotFoundException => (StatusCodes.Status404NotFound, ex.Message, null),
            ConflictException => (StatusCodes.Status409Conflict, ex.Message, null),
            BadRequestException => (StatusCodes.Status400BadRequest, ex.Message, null),
            _ => (StatusCodes.Status500InternalServerError,"An unexpected error occurred.", null)
        };

        if (status == StatusCodes.Status500InternalServerError)
            logger.LogError(ex, "Unhandled exception for {Path}", context.Request.Path);
        else
            logger.LogWarning("Handled {Exception}: {Message}", ex.GetType().Name, ex.Message);

        var payload = new ApiError
        {
            Status = status,
            Message = message,
            Errors = errors,
            TraceId = context.TraceIdentifier
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}