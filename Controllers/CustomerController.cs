using FluentValidationDemo.Features.Customers.Add;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ISender sender;

    public CustomerController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        await this.sender.Send(request);

        return Ok("Success!");
    }
}