using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Domain.Entities;

public class CustomerDocument
{
    [Key]
    public int DocumentId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string DocumentType { get; set; } = string.Empty;

    public string? DocumentNumber { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    [Required]
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string VerificationStatus { get; set; } = "Pending"; // Pending, Verified, Rejected

    public Customer? Customer { get; set; }
}
