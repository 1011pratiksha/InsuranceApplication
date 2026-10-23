
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/dashboard/analytics")]
  public class DashboardAnalyticsController : ControllerBase
  {
    private readonly IDashboardAnalyticsService service;

    public DashboardAnalyticsController(
        IDashboardAnalyticsService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardAnalytics()
    {
      try
      {
        var result = await service
            .GetDashboardAnalyticsAsync();

        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, "Dashboard analytics fetch karte waqt error aaya.");
      }
    }
  }
}
