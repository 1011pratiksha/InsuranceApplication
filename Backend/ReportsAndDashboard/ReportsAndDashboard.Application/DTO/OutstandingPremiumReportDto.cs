
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class OutstandingPremiumReportDto
  {
    public long ScheduleId { get; set; }

    public long PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public int InstallmentNo { get; set; }

    public DateTime DueDate { get; set; }

    public decimal PremiumAmount { get; set; }

    public decimal LateFee { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal AmountPaid { get; set; }

    public decimal OutstandingAmount { get; set; }
  }
}
