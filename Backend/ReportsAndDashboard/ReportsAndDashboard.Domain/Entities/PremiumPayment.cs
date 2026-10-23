using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  [Table("premium_payment")]
  public class PremiumPayment
  {
    [Key]
    public long PaymentId { get; set; }

    [ForeignKey(nameof(PremiumSchedule))]
    public long ScheduleId { get; set; }

    [ForeignKey(nameof(Policy))]
    public long PolicyId { get; set; }

    public DateTime? PaymentDate { get; set; }

    [Column(TypeName = "decimal(15,2)")]
    public decimal AmountPaid { get; set; }

    [MaxLength(30)]
    public string PaymentMode { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? TransactionReference { get; set; }

    [MaxLength(30)]
    public string PaymentStatus { get; set; } = "Pending";

    public PremiumSchedule PremiumSchedule { get; set; } = null!;

    public Policy Policy { get; set; } = null!;
  }
}
