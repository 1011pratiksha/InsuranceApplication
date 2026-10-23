using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Domain.Entities;

public class Policy
{
    [Key]
    public int PolicyId { get; set; }

    [Required]
    public string PolicyNumber { get; set; } = string.Empty;

    [Required]
    public int QuoteId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public DateTime IssueDate { get; set; }

    [Required]
    public DateTime EffectiveDate { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    [Required]
    public string PolicyStatus { get; set; } = string.Empty;

    public List<PolicyCoverage> PolicyCoverages { get; set; } = new();
    public List<PolicyRider> PolicyRiders { get; set; } = new();
    public List<PolicyDocument> PolicyDocuments { get; set; } = new();
    public List<PolicyEndorsement> PolicyEndorsements { get; set; } = new();
    public List<PolicyHistory> PolicyHistories { get; set; } = new();
}
