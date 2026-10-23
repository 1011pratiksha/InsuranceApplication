using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Domain.Entities;

public class CustomerAddress
{
    [Key]
    public int AddressId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string AddressType { get; set; } = "Permanent"; // Permanent or Communication

    [Required]
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string District { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string Pincode { get; set; } = string.Empty;

    public Customer? Customer { get; set; }
}
