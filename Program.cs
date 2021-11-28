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
    Console.WriteLine($"Customer {customer.Name} saved.");    
}
else 
{
    Console.WriteLine("Invalid customer.");
}

bool IsValid(Customer customer)
{
    /// ???
    return true;
}