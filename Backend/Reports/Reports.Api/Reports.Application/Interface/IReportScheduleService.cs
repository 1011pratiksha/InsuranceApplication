using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Interface
{
    public interface IReportScheduleService
    {
        Task<ReportScheduleDto> CreateAsync(
            CreateReportScheduleDto dto);

        Task<ReportScheduleDto?> UpdateAsync(
            long id,
            UpdateReportScheduleDto dto);

        Task<bool> DeleteAsync(long id);

        Task<List<ReportScheduleDto>> GetAllAsync();
    }
}
