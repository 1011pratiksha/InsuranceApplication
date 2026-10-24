using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Domain.Entities;

public class Nominee
{
    [Key]
    public int NomineeId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string NomineeName { get; set; } = string.Empty;

    [Required]
    public string Relationship { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    public string? MobileNumber { get; set; }

    [Required]
    public decimal SharePercentage { get; set; }

    public string? GuardianName { get; set; }

    public Customer? Customer { get; set; }
}
