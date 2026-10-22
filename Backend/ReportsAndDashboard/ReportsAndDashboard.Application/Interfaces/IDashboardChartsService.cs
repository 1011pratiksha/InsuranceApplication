
using ReportsAndDashboard.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IDashboardChartsService
  {
    Task<List<DashboardChartDto>> GetDashboardChartsAsync();
  }
}
