
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.Interfaces;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/customers")]
  public class CustomerReportsController : ControllerBase
  {
    private readonly ICustomerReportService service;

    public CustomerReportsController(
        ICustomerReportService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerReport()
    {
      var data = await service.GetCustomerReportAsync();

      return Ok(data);
    }
  }
}
