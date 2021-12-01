using System.ComponentModel.DataAnnotations;

var customer = new Customer
{
    Name = "P",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
};

Validate(customer);

void Validate(Customer customer)
{
    var context = new ValidationContext(customer, serviceProvider: null, items: null);
    var errorResults = new List<ValidationResult>();

    // carry out validation.
    var isValid = Validator.TryValidateObject(customer, context, errorResults, true);

    if (isValid)
    {
        System.Console.WriteLine($"Model is valid.");
    }
    else
    {
        foreach (var error in errorResults)
        {
            System.Console.WriteLine($"{string.Join(", ", error.MemberNames)}: {error.ErrorMessage}");
        }
    }
}