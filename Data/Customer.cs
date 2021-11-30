namespace FluentValidationDemo.Data;

public class Customer 
{
  public string Name { get; set; }

  public bool HasDiscount { get; set; }

  public decimal Discount { get; set; }

  public Country Country { get; set; }

  public string Postcode { get; set; }
}
