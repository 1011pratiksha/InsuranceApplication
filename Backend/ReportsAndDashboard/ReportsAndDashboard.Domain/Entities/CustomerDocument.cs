
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class CustomerDocument
  {
    [Key]
    public long CustomerDocumentId { get; set; }

    public long CustomerId { get; set; }

    public long DocumentTypeId { get; set; }

    public string FilePath { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; }

    public string VerificationStatus { get; set; } = "Pending";

    // Foreign key relationships

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(DocumentTypeId))]
    public DocumentType DocumentType { get; set; } = null!;
  }
}
