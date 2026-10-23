using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/premiums")]
  public class PremiumReportsController : ControllerBase
  {
    private readonly IPremiumReportService service;

    public PremiumReportsController(IPremiumReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPremiumReport()
    {
      var data = await service.GetPremiumReportAsync();

      return Ok(data);
    }
  }
}
