
using ReportsAndDashboard.Application.DTO;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IDashboardSummaryService
  {
    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
  }
}
