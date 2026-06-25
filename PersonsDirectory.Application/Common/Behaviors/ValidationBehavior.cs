using MediatR;
using PersonsDirectory.Application.Common.Validation;
using AppValidationException = PersonsDirectory.Application.Common.Exceptions.ValidationException;

namespace PersonsDirectory.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    
    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        foreach (var validator in _validators)
        {
            var result = validator.Validate(request);
            if (!result.IsValid)
                throw new AppValidationException(result.ToDictionary());
        }

        return await next(ct);
    }
}