
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class FinancialReportDto
  {
    public long PaymentId { get; set; }

    public long PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public DateTime? PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }

    public string PaymentMode { get; set; } = string.Empty;

    public string? TransactionReference { get; set; }

    public string PaymentStatus { get; set; } = string.Empty;
  }
}
