using ReportsAndDashboard.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IPremiumReportService
  {
    Task<List<PremiumReportDto>> GetPremiumReportAsync();
  }
}
