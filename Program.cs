using System.ComponentModel.DataAnnotations;

var customer = new Customer
{
    Name = "Peter Pan",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
};

var context = new ValidationContext(customer, serviceProvider: null, items: null);
var errorResults = new List<ValidationResult>();

if (Validator.TryValidateObject(customer, context, errorResults, true))
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