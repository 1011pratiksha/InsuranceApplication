using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class ClaimReportDto
  {
    public long ClaimId { get; set; }

    public string ClaimNumber { get; set; } = string.Empty;

    public long PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string ClaimType { get; set; } = string.Empty;

    public DateTime IncidentDate { get; set; }

    public DateTime ClaimDate { get; set; }

    public decimal EstimatedLoss { get; set; }

    public string ClaimStatus { get; set; } = string.Empty;
  }
}
