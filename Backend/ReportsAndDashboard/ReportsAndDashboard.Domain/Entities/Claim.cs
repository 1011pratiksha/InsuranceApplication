using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  [Table("claim_master")]
  public class Claim
  {
    [Key]
    [Column("claim_id")]
    public long ClaimId { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("claim_number")]
    public string ClaimNumber { get; set; } = string.Empty;

    [Column("policy_id")]
    public long PolicyId { get; set; }

    [Column("customer_id")]
    public long CustomerId { get; set; }

    [MaxLength(50)]
    [Column("claim_type")]
    public string ClaimType { get; set; } = string.Empty;

    [Column("incident_date", TypeName = "date")]
    public DateTime IncidentDate { get; set; }

    [Column("claim_date", TypeName = "date")]
    public DateTime ClaimDate { get; set; }

    [Column("estimated_loss", TypeName = "decimal(15,2)")]
    public decimal EstimatedLoss { get; set; }

    [MaxLength(30)]
    [Column("claim_status")]
    public string ClaimStatus { get; set; } = "Registered";

    // Foreign key relationships

    [ForeignKey(nameof(PolicyId))]
    public Policy Policy { get; set; } = null!;

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
  }
}
