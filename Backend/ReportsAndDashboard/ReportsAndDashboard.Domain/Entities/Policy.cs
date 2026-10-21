
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class Policy
  {
    [Key]
    public long PolicyId { get; set; }

    public long CustomerId { get; set; }

    public long ProductId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public string PolicyStatus { get; set; } = "Active";

    public DateTime? IssueDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    // Foreign key relationships

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public InsuranceProduct Product { get; set; } = null!;
  }
}
