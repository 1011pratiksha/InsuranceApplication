
using ReportsAndDashboard.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IOutstandingPremiumReportService
  {
    Task<List<OutstandingPremiumReportDto>> GetOutstandingPremiumReportAsync(
        DateTime? fromDate,
        DateTime? toDate);
  }
}
