using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/policies")]
  public class PolicyReportsController : ControllerBase
  {
    private readonly IPolicyReportService service;

    public PolicyReportsController(IPolicyReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPolicyReport()
    {
      var data = await service.GetPolicyReportAsync();

      return Ok(data);
    }
  }
}
