
using ReportsAndDashboard.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IReportScheduleService
  {
    Task<List<ReportScheduleDto>> GetAllSchedulesAsync();

    Task<ReportScheduleDto?> GetScheduleByIdAsync(long id);

    Task<ReportScheduleDto> CreateScheduleAsync(ReportScheduleDto dto);

    Task<bool> UpdateScheduleAsync(long id, ReportScheduleDto dto);

    Task<bool> DeleteScheduleAsync(long id);
  }
}
