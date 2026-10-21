
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [Route("api/v1/reports/financial")]
  [ApiController]
  public class FinancialReportController : ControllerBase
  {
    private readonly IFinancialReportService service;

    public FinancialReportController(
        IFinancialReportService service)
    {
      this.service = service;
    }

    [HttpGet("premium-income")]
    public async Task<IActionResult> GetPremiumIncomeReport(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
      if (fromDate.HasValue &&
          toDate.HasValue &&
          fromDate.Value.Date > toDate.Value.Date)
      {
        return BadRequest("fromDate cannot be later than toDate.");
      }

      var result =
          await service.GetPremiumIncomeReportAsync(
              fromDate, toDate);

      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetFinancialReportSummary()
    {
      var result =
          await service.GetFinancialReportSummaryAsync();

      return Ok(result);
    }
  }
}
