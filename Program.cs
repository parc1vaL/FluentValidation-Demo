var customer = new Customer
{
    Name = "Peter Pan",
    Country = Country.Canada,
    HasDiscount = true,
    Discount = 0.0m,
    Postcode = "Neverland",
};

if (IsValid(customer))
{
    System.Console.WriteLine($"Customer {customer.Name} saved.");    
}

bool IsValid(Customer customer)
{
    /// ???
    return true;
}