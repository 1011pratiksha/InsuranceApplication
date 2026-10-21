
using ReportsAndDashboard.Application.DTO;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IDashboardKpiService
  {
    Task<DashboardKpiDto> GetDashboardKpiAsync();
  }
}
