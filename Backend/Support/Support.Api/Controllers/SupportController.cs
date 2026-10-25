
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportController : ControllerBase
  {
    private readonly ISupportService service;
    private readonly ILogger<SupportController> logger;

    public SupportController(
        ISupportService service,
        ILogger<SupportController> logger)
    {
      this.service = service;
      this.logger = logger;
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
      if (id <= 0)
        return BadRequest(new { message = "Invalid ticket ID." });

      var ticket = await service.GetTicketById(id);

      if (ticket == null)
        return NotFound(new { message = "Support ticket not found." });

      return Ok(ticket);
    }

    // GET: api/Support/customer/1
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetTicketsByCustomerId(int customerId)
    {
      if (customerId <= 0)
        return BadRequest(new { message = "Invalid customer ID." });

      var tickets = await service.GetTicketsByCustomerId(customerId);
      return Ok(tickets);
    }

    // POST: api/Support
    [HttpPost]
    public async Task<IActionResult> CreateTicket(
        [FromBody] SupportTicketDto ticket)
    {
      if (ticket == null)
        return BadRequest(new { message = "Ticket data is required." });

      if (ticket.CustomerId <= 0)
        return BadRequest(new { message = "Invalid customer ID." });

      if (string.IsNullOrWhiteSpace(ticket.Subject))
        return BadRequest(new { message = "Subject is required." });

      try
      {
        var createdTicket = await service.CreateTicket(ticket);

        return CreatedAtAction(
            nameof(GetTicketById),
            new { id = createdTicket.TicketId },
            createdTicket);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
      catch (HttpRequestException ex)
      {
        logger.LogError(ex, "Customer API request failed.");

        return StatusCode(503, new
        {
          message = "Customer service is unavailable."
        });
      }
      catch (TaskCanceledException ex)
      {
        logger.LogError(ex, "Customer API request timed out.");

        return StatusCode(503, new
        {
          message = "Customer service request timed out."
        });
      }
    }

    // PUT: api/Support/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(
        int id,
        [FromBody] SupportTicketDto ticket)
    {
      if (id <= 0)
        return BadRequest(new { message = "Invalid ticket ID." });

      if (ticket == null)
        return BadRequest(new { message = "Ticket data is required." });

      if (ticket.CustomerId <= 0)
        return BadRequest(new { message = "Invalid customer ID." });

      try
      {
        var updatedTicket = await service.UpdateTicket(id, ticket);

        if (updatedTicket == null)
          return NotFound(new { message = "Support ticket not found." });

        return Ok(updatedTicket);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
      catch (HttpRequestException ex)
      {
        logger.LogError(ex, "Customer API request failed during ticket update.");

        return StatusCode(503, new
        {
          message = "Customer service is unavailable."
        });
      }
      catch (TaskCanceledException ex)
      {
        logger.LogError(ex, "Customer API request timed out during ticket update.");

        return StatusCode(503, new
        {
          message = "Customer service request timed out."
        });
      }
    }

    // DELETE: api/Support/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
      if (id <= 0)
        return BadRequest(new { message = "Invalid ticket ID." });

      var deleted = await service.DeleteTicket(id);

      if (!deleted)
        return NotFound(new { message = "Support ticket not found." });

      return Ok(new
      {
        message = "Support ticket deleted successfully."
      });
    }
  }
}
