
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/dashboard/summary")]
  public class DashboardSummaryController : ControllerBase
  {
    private readonly IDashboardSummaryService service;

    public DashboardSummaryController(
        IDashboardSummaryService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardSummary()
    {
      try
      {
        var result = await service
            .GetDashboardSummaryAsync();

        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, new
        {
          message = "An error occurred while fetching dashboard summary."
        });
      }
    }
  }
}
