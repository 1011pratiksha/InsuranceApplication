
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  public class CustomerNominee
  {
    [Key]
    public long CustomerNomineeId { get; set; }

    public long CustomerId { get; set; }

    public long RelationshipId { get; set; }

    public string NomineeName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string MobileNumber { get; set; } = string.Empty;

    public decimal SharePercentage { get; set; }

    public string? GuardianName { get; set; }

    // Foreign key relationships

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(RelationshipId))]
    public Relationship Relationship { get; set; } = null!;
  }
}
