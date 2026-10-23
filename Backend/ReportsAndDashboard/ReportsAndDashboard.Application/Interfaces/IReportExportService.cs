using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsAndDashboard.Domain.Entities.Reporting;

namespace ReportsAndDashboard.Application.Interfaces
{
  public interface IReportExportService
  {
    Task<byte[]> ExportCsvAsync();
    Task<byte[]> ExportExcelAsync();
    Task<byte[]> ExportPdfAsync();

    Task<List<ReportExecution>> GetExportHistoryAsync();
  }
}
