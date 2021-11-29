using MediatR;

namespace FluentValidationDemo.Features.Customers.Add;

public class AddCustomerHandler : AsyncRequestHandler<AddCustomerCommand>
{
    private readonly ILogger<AddCustomerHandler> logger;

    public AddCustomerHandler(ILogger<AddCustomerHandler> logger)
    {
        this.logger = logger;
    }

    protected override async Task Handle(
        AddCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Handling request...");
        // Handle request...
    }
}