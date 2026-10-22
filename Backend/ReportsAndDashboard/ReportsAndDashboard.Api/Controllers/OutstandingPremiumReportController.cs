
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [Route("api/v1/reports/financial/outstanding-premium")]
  [ApiController]
  public class OutstandingPremiumReportController : ControllerBase
  {
    private readonly IOutstandingPremiumReportService service;

    public OutstandingPremiumReportController(
        IOutstandingPremiumReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetOutstandingPremiumReport(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
      if (fromDate.HasValue &&
          toDate.HasValue &&
          fromDate.Value.Date > toDate.Value.Date)
      {
        return BadRequest(
            "fromDate cannot be later than toDate.");
      }

      var result = await service.GetOutstandingPremiumReportAsync(
          fromDate,
          toDate);

      return Ok(result);
    }
  }
}
