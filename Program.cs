using System.ComponentModel.DataAnnotations;

var customer = new Customer
{
    Name = "Peter Pan",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
    Orders =
    {
        new Order { OrderTotal = 0.0m, },
    }
};

var context = new ValidationContext(customer, serviceProvider: null, items: null);
var errorResults = new List<ValidationResult>();

if (Validator.TryValidateObject(customer, context, errorResults, true))
{
    Console.WriteLine($"Model is valid.");
}
else
{
    foreach (var error in errorResults)
    {
        Console.WriteLine($"{string.Join(", ", error.MemberNames)}: {error.ErrorMessage}");
    }
}