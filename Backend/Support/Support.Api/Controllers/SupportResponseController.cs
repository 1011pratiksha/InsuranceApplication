
using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Interfaces;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportResponseController : ControllerBase
  {
    private readonly ISupportResponseService service;

    public SupportResponseController(ISupportResponseService service)
    {
      this.service = service;
    }

    // GET: api/SupportResponse
    [HttpGet]
    public async Task<IActionResult> GetAllResponses()
    {
      var responses = await service.GetAllResponses();
      return Ok(responses);
    }

    // GET: api/SupportResponse/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResponseById(int id)
    {
      var response = await service.GetResponseById(id);

      if (response == null)
        return NotFound("Support response not found.");

      return Ok(response);
    }

    // GET: api/SupportResponse/ticket/1
    [HttpGet("ticket/{ticketId}")]
    public async Task<IActionResult> GetResponsesByTicketId(int ticketId)
    {
      var responses = await service.GetResponsesByTicketId(ticketId);
      return Ok(responses);
    }

    // POST: api/SupportResponse
    [HttpPost]
    public async Task<IActionResult> CreateResponse(SupportResponseDto response)
    {
      var createdResponse = await service.CreateResponse(response);

      return CreatedAtAction(
          nameof(GetResponseById),
          new { id = createdResponse.ResponseId },
          createdResponse);
    }

    // PUT: api/SupportResponse/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResponse(int id, SupportResponseDto response)
    {
      var updatedResponse = await service.UpdateResponse(id, response);

      if (updatedResponse == null)
        return NotFound("Support response not found.");

      return Ok(updatedResponse);
    }

    // DELETE: api/SupportResponse/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResponse(int id)
    {
      var deleted = await service.DeleteResponse(id);

      if (!deleted)
        return NotFound("Support response not found.");

      return Ok("Support response deleted successfully.");
    }
  }
}
