using ReportsAndDashboard.Application.DTO;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IPolicyReportService
  {
    Task<List<PolicyReportDto>> GetPolicyReportAsync();
  }
}
