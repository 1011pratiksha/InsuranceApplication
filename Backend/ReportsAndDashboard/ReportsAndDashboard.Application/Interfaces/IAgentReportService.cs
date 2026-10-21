
using ReportsAndDashboard.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IAgentReportService
  {
    Task<List<AgentReportDto>> GetAgentReportAsync();
  }
}
