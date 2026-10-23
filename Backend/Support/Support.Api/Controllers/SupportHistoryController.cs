
using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Interfaces;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportHistoryController : ControllerBase
  {
    private readonly ISupportHistoryService service;

    public SupportHistoryController(ISupportHistoryService service)
    {
      this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHistory()
    {
      var history = await service.GetAllHistory();
      return Ok(history);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHistoryById(int id)
    {
      var history = await service.GetHistoryById(id);

      if (history == null)
      {
        return NotFound();
      }

      return Ok(history);
    }

    [HttpGet("ticket/{ticketId}")]
    public async Task<IActionResult> GetHistoryByTicketId(int ticketId)
    {
      var history = await service.GetHistoryByTicketId(ticketId);
      return Ok(history);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHistory(SupportHistoryDto history)
    {
      var result = await service.CreateHistory(history);

      return CreatedAtAction(
          nameof(GetHistoryById),
          new { id = result.HistoryId },
          result
      );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHistory(
        int id,
        SupportHistoryDto history)
    {
      var result = await service.UpdateHistory(id, history);

      if (result == null)
      {
        return NotFound();
      }

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHistory(int id)
    {
      var result = await service.DeleteHistory(id);

      if (!result)
      {
        return NotFound();
      }

      return NoContent();
    }
  }
}
