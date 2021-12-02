using FluentValidation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .Length(2, 200);

        RuleFor(c => c.Country)
            .IsInEnum();

        RuleFor(c => c.Postcode)
            .NotEmpty()
            .Matches("[0-9]{5}");

        RuleForEach(c => c.Orders)
            .NotNull()
            .ChildRules(order =>
            {
                order.RuleFor(o => o.OrderTotal).GreaterThan(0.0m);
            });
    }
}