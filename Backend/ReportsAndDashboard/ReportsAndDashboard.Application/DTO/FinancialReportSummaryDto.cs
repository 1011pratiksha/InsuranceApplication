
using System;

namespace ReportsAndDashboard.Application.DTO
{
  public class FinancialReportSummaryDto
  {
    public DateTime ReportDate { get; set; }

    public decimal TotalPremiumIncome { get; set; }

    public decimal TotalOutstandingPremium { get; set; }
  }
}
