using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonsDirectory.WebApi.Models;

namespace PersonsDirectory.WebApi.Filters;

public sealed class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context.ModelState
            .Where(kv => kv.Value!.Errors.Count > 0)
            .ToDictionary(
                kv => kv.Key,
                kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        context.Result = new BadRequestObjectResult(new ApiError
        {
            Status = StatusCodes.Status400BadRequest,
            Message = "One or more validation errors occurred.",
            Errors = errors,
            TraceId = context.HttpContext.TraceIdentifier
        });
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}