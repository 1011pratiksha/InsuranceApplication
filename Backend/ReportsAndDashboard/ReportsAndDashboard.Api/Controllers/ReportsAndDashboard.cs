
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/dashboard/kpi")]
  public class DashboardKpiController : ControllerBase
  {
    private readonly IDashboardKpiService service;

    public DashboardKpiController(
        IDashboardKpiService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardKpi()
    {
      try
      {
        var result = await service
            .GetDashboardKpiAsync();

        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, new
        {
          message = "An error occurred while fetching dashboard KPIs."
        });
      }
    }
  }
}
