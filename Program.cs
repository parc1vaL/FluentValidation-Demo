
var customer = new Customer
{
    Name = "P",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
    Orders =
    {
        new Order { OrderTotal = -10.0m, },
    }
};

var validator = new CustomerValidator();
var validationResult = validator.Validate(customer);

if (validationResult.IsValid)
{
    System.Console.WriteLine("Success");
}
else
{
    foreach (var error in validationResult.Errors)
    {
        System.Console.WriteLine($"{error.PropertyName}: {error.ErrorMessage}");
    }
}