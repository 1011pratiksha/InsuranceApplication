
using Microsoft.AspNetCore.Mvc;
using ReportsAndDashboard.Application.DTO;
using ReportsAndDashboard.Application.Interfaces;
using System.Threading.Tasks;

namespace ReportsAndDashboard.Api.Controllers
{
  [ApiController]
  [Route("api/v1/reports/schedule")]
  public class ReportScheduleController : ControllerBase
  {
    private readonly IReportScheduleService service;

    public ReportScheduleController(IReportScheduleService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var result = await service.GetAllSchedulesAsync();
      return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
      var result = await service.GetScheduleByIdAsync(id);

      if (result == null)
        return NotFound(new { message = "Schedule not found." });

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ReportScheduleDto dto)
    {
      var result = await service.CreateScheduleAsync(dto);

      return CreatedAtAction(
          nameof(GetById),
          new { id = result.ScheduleId },
          result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] ReportScheduleDto dto)
    {
      var updated = await service.UpdateScheduleAsync(id, dto);

      if (!updated)
        return NotFound(new { message = "Schedule not found." });

      return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
      var deleted = await service.DeleteScheduleAsync(id);

      if (!deleted)
        return NotFound(new { message = "Schedule not found." });

      return NoContent();
    }
  }
}
