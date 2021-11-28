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

        RuleFor(c => c.Postcode)
            .NotEmpty().WithMessage("{PropertyName} must not be empty.")
            .Matches("[0-9]{5}").WithMessage("Postcode must consist of 5 digits.");
    }
}