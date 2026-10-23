
using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Interfaces;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportController : ControllerBase
  {
    private readonly ISupportService service;
    public SupportController(ISupportService service)
    {
      this.service = service;
    }

    // GET: api/Support
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
      var tickets = await service.GetAllTickets();
      return Ok(tickets);
    }

    // GET: api/Support/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
      var ticket = await service.GetTicketById(id);

      if (ticket == null)
        return NotFound("Support ticket not found.");

      return Ok(ticket);
    }

    // GET: api/Support/customer/1
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetTicketsByCustomerId(int customerId)
    {
      var tickets = await service.GetTicketsByCustomerId(customerId);
      return Ok(tickets);
    }

    // POST: api/Support
    [HttpPost]
    public async Task<IActionResult> CreateTicket(SupportTicketDto ticket)
    {
      var createdTicket = await service.CreateTicket(ticket);

      return CreatedAtAction(
          nameof(GetTicketById),
          new { id = createdTicket.TicketId },
          createdTicket);
    }

    // PUT: api/Support/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(
        int id, SupportTicketDto ticket)
    {
      var updatedTicket = await service.UpdateTicket(id, ticket);

      if (updatedTicket == null)
        return NotFound("Support ticket not found.");

      return Ok(updatedTicket);
    }

    // DELETE: api/Support/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
      var deleted = await service.DeleteTicket(id);

      if (!deleted)
        return NotFound("Support ticket not found.");

      return Ok("Support ticket deleted successfully.");
    }
  }
}
