using FluentValidationDemo.Common.Enums;
using MediatR;

namespace FluentValidationDemo.Features.Customers.Add;

public class AddCustomerCommand : IRequest
{
    public string Name { get; set; }

    public bool HasDiscount { get; set; }

    public decimal Discount { get; set; }

    public Country Country { get; set; }
    
    public string Postcode { get; set; }
}