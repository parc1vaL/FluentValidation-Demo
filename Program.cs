var customer = new Customer
{
    Name = "P",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
};

var validator = new CustomerValidator();
var validationResult = validator.Validate(customer);

if (validationResult.IsValid)
{
    Console.WriteLine($"Customer {customer.Name} saved.");    
}
else 
{
    Console.WriteLine("Invalid customer.");

    foreach (var validationError in validationResult.Errors)
    {
        Console.WriteLine(validationError.ErrorMessage);
    }
}