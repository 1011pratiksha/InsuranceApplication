using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Domain.Entities;

public class CustomerContact
{
    [Key]
    public int ContactId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public string? AlternateMobile { get; set; }

    public string? OfficePhone { get; set; }

    [Required]
    public string PreferredContactMode { get; set; } = "Mobile"; // Mobile, Email, SMS

    public Customer? Customer { get; set; }
}
