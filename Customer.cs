using System.ComponentModel.DataAnnotations;

public class Customer 
{
  [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters.")]
  public string Name { get; set; }

  public bool HasDiscount { get; set; }

  public decimal Discount { get; set; }

  public Country Country { get; set; }

  [Required(ErrorMessage = "Postcode is required.")]
  public string Postcode { get; set; }
}
