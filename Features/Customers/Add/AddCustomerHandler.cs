using MediatR;

namespace FluentValidationDemo.Features.Customers.Add;

public class AddCustomerHandler : AsyncRequestHandler<AddCustomerCommand>
{
    protected override async Task Handle(
        AddCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        // Handle request...
    }
}