
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/underwriting")]
  public class UnderwritingReportsController : ControllerBase
  {
    private readonly IUnderwritingReportService service;

    public UnderwritingReportsController(
        IUnderwritingReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetUnderwritingReport()
    {
      var data = await service
          .GetUnderwritingReportAsync();

      return Ok(data);
    }
  }
}
