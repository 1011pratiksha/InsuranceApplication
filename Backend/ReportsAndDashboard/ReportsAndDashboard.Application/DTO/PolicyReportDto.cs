using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class PolicyReportDto
  {
    public long PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public long ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string PolicyStatus { get; set; } = string.Empty;

    public DateTime? IssueDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
  }
}
