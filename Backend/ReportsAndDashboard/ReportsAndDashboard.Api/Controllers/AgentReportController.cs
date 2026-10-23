
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [Route("api/v1/reports/agents")]
  [ApiController]
  public class AgentReportController : ControllerBase
  {
    private readonly IAgentReportService service;

    public AgentReportController(
        IAgentReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAgentReport()
    {
      var result = await service.GetAgentReportAsync();

      return Ok(result);
    }
  }
}
