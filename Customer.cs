using System.ComponentModel.DataAnnotations;

public class Customer 
{
  [Required(ErrorMessage = "Name is required.")]
  [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters.")]
  public string Name { get; set; }

  public bool HasDiscount { get; set; }

  public decimal Discount { get; set; }

  [EnumDataType(typeof(Country))]
  public Country Country { get; set; }

  [Required(ErrorMessage = "Postcode is required.")]
  [RegularExpression("^[0-9]{5}$", ErrorMessage = "Invalid postcode format.")]
  public string Postcode { get; set; }
}
