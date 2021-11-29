using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FluentValidationDemo.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest> validator;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;

    public ValidationBehavior(
        IValidator<TRequest> validator,
        ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        this.validator = validator;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            await this.validator.ValidateAndThrowAsync(request, cancellationToken);
        }
        catch
        {
            this.logger.LogError("Validation failed.");
            throw;
        }

        this.logger.LogInformation("Validation successful.");

        return await next();
    }
}