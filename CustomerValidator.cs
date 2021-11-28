using System.Text.RegularExpressions;
using FluentValidation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // Stop validation on first error of each property
        CascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .Length(2, 200).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} character(s).");

        RuleFor(c => c.Country)
            .IsInEnum();

        RuleFor(c => c.Discount)
            .NotEqual(0.0m).When(c => c.HasDiscount).WithMessage("If a discount is selected, it must not be zero.");

        RuleFor(c => c.Postcode)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .Must(BeAValidPostcode).WithMessage(c => $"{c.Postcode} is not a valid postcode in {c.Country}.");
    }

    private static bool BeAValidPostcode(Customer customer, string postcode)
    {
        return customer.Country switch
        {
            Country.Canada => Regex.Match(postcode, "[0-9]{4}").Success,
            Country.USA => Regex.Match(postcode, "[A-Z]{2}-[0-9]{5}").Success,
            Country.Germany => Regex.Match(postcode, "[0-9]{5}").Success,
            _ => false,
        };
    }
}