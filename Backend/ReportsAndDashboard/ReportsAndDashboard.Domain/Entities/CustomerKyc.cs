
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class CustomerKyc
  {
    [Key]
    public long CustomerKycId { get; set; }

    public long CustomerId { get; set; }

    public long DocumentTypeId { get; set; }

    public string DocumentNumber { get; set; } = string.Empty;

    public string VerificationStatus { get; set; } = "Pending";

    public DateTime SubmittedAt { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public string? Remarks { get; set; }

    // Foreign key relationships

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(DocumentTypeId))]
    public DocumentType DocumentType { get; set; } = null!;
  }
}
