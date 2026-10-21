using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsAndDashboard.Domain.Entities
{
  [Table("premium_schedule")]
  public class PremiumSchedule
  {
   
    [Key]
    public long ScheduleId { get; set; }

    [ForeignKey(nameof(Policy))]
    public long PolicyId { get; set; }

    public int InstallmentNo { get; set; }

    public DateTime DueDate { get; set; }

    [Column(TypeName = "decimal(15,2)")]
    public decimal PremiumAmount { get; set; }

    [Column(TypeName = "decimal(15,2)")]
    public decimal LateFee { get; set; } = 0;

    [MaxLength(30)]
    public string Status { get; set; } = "Pending";

    public Policy Policy { get; set; } = null!;
  }
}
