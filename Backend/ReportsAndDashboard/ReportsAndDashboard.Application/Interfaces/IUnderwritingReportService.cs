
using ReportsAndDashboard.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IUnderwritingReportService
  {
    Task<List<UnderwritingReportDto>> GetUnderwritingReportAsync();
  }
}
