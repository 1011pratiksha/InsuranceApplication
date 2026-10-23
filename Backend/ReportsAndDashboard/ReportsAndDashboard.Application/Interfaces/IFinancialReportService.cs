using ReportsAndDashboard.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IFinancialReportService
  {
    Task<List<FinancialReportDto>> GetPremiumIncomeReportAsync(
        DateTime? fromDate,
        DateTime? toDate);

    Task<FinancialReportSummaryDto> GetFinancialReportSummaryAsync();
  }
}
