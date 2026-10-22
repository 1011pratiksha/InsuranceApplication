
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  [Table("underwriting_case")]
  public class UnderwritingCase
  {
    [Key]
    public long UnderwritingId { get; set; }

    public long QuoteId { get; set; }

    public long CustomerId { get; set; }

    public long ProductId { get; set; }

    public DateTime UnderwritingDate { get; set; }

    [MaxLength(30)]
    public string Status { get; set; } = "Pending";

    public Customer Customer { get; set; } = null!;

    public InsuranceProduct Product { get; set; } = null!;
  }
}
