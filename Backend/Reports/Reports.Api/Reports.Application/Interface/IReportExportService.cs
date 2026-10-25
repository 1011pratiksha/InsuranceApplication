using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Interface
{
    public interface IReportExportService
    {
        Task<byte[]> ExportPdfAsync(
         string reportType,
         ReportFilterDto filter);

        Task<byte[]> ExportExcelAsync(
            string reportType,
            ReportFilterDto filter);

        Task<byte[]> ExportCsvAsync(
            string reportType,
            ReportFilterDto filter);
    }
}
