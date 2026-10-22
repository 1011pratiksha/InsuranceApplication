using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class UnderwritingReportDto
  {
    public long UnderwritingId { get; set; }

    public long QuoteId { get; set; }

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public long ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public DateTime UnderwritingDate { get; set; }

    public string Status { get; set; } = string.Empty;
  }
}
