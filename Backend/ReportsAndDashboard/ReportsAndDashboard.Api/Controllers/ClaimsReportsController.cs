using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/claims")]
  public class ClaimsReportsController : ControllerBase
  {
    private readonly IClaimReportService service;

    public ClaimsReportsController(
        IClaimReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetClaimReport()
    {
      var data = await service.GetClaimReportAsync();

      return Ok(data);
    }
  }
}
