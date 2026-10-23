using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class PremiumReportDto
  {
    public long ScheduleId { get; set; }

    public long PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int InstallmentNo { get; set; }

    public DateTime DueDate { get; set; }

    public decimal PremiumAmount { get; set; }

    public decimal LateFee { get; set; }

    public string ScheduleStatus { get; set; } = string.Empty;

    public long? PaymentId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? AmountPaid { get; set; }

    public string? PaymentMode { get; set; }

    public string? TransactionReference { get; set; }

    public string? PaymentStatus { get; set; }
  }
}
