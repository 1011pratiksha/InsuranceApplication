
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/dashboard/charts")]
  public class DashboardChartsController : ControllerBase
  {
    private readonly IDashboardChartsService service;

    public DashboardChartsController(
        IDashboardChartsService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardCharts()
    {
      try
      {
        var result = await service
            .GetDashboardChartsAsync();

        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, new
        {
          message = "An error occurred while fetching dashboard charts."
        });
      }
    }
  }
}
