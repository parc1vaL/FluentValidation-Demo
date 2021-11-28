using FluentValidation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // Stop validation on first error of each property
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Name)
            .NotEmpty()
            .Length(2, 200);

        RuleFor(c => c.Country)
            .IsInEnum();

        RuleFor(c => c.Postcode)
            .NotEmpty()
            .Matches("[0-9]{5}");
    }
}