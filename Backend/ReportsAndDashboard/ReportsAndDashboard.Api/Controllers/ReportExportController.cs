
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/export")]
  public class ReportExportController : ControllerBase
  {
    private readonly IReportExportService service;

    public ReportExportController(
        IReportExportService service)
    {
      this.service = service;
    }

    [HttpGet("csv")]
    public async Task<IActionResult> ExportCsv()
    {
      var fileBytes =
          await service.ExportCsvAsync();

      return File(
          fileBytes,
          "text/csv; charset=utf-8",
          "dashboard-analytics.csv");
    }

    [HttpGet("history/{executionId:long}/download")]
    public async Task<IActionResult> DownloadReport(long executionId)
    {
      var history = await service
          .GetExportHistoryAsync();

      var execution = history.FirstOrDefault(
          x => x.ExecutionId == executionId);

      if (execution == null)
        return NotFound(new { message = "Report history not found." });

      if (execution.ExecutionStatus !=
          ReportsAndDashboard.Domain.Entities.Reporting.ReportExecutionStatus.Success)
      {
        return BadRequest(new { message = "Report execution was not successful." });
      }

      if (string.IsNullOrWhiteSpace(execution.FilePath))
        return NotFound(new { message = "No file is available for this execution." });

      var generatedDirectory = Path.GetFullPath(
          Path.Combine(AppContext.BaseDirectory, "GeneratedReports"));

      var fileName = Path.GetFileName(execution.FilePath);

      // Only allow files inside GeneratedReports.
      var fullPath = Path.GetFullPath(
          Path.Combine(generatedDirectory, fileName));

      if (!fullPath.StartsWith(
              generatedDirectory + Path.DirectorySeparatorChar,
              StringComparison.OrdinalIgnoreCase))
      {
        return BadRequest(new { message = "Invalid report file path." });
      }

      if (!System.IO.File.Exists(fullPath))
        return NotFound(new { message = "Report file not found on server." });

      var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);

      return File(bytes, "application/pdf", fileName);
    }

    [HttpGet("excel")]
    public async Task<IActionResult> ExportExcel()
    {
      var fileBytes =
          await service.ExportExcelAsync();

      return File(
          fileBytes,
          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          "dashboard-analytics.xlsx");
    }

    [HttpGet("pdf")]
    public async Task<IActionResult> ExportPdf()
    {
      var fileBytes =
          await service.ExportPdfAsync();

      return File(
          fileBytes,
          "application/pdf",
          "dashboard-analytics.pdf");
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetExportHistory()
    {
      var history = await service.GetExportHistoryAsync();
      return Ok(history);
    }
  }
}
