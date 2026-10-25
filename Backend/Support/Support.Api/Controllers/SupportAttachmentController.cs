
using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Interfaces;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportAttachmentController : ControllerBase
  {
    private readonly ISupportAttachmentService service;

    public SupportAttachmentController(ISupportAttachmentService service)
    {
      this.service = service;
    }

    // GET: api/SupportAttachment
    [HttpGet]
    public async Task<IActionResult> GetAllAttachments()
    {
      var attachments = await service.GetAllAttachments();
      return Ok(attachments);
    }

    // GET: api/SupportAttachment/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttachmentById(int id)
    {
      var attachment = await service.GetAttachmentById(id);

      if (attachment == null)
        return NotFound("Attachment not found.");

      return Ok(attachment);
    }

    // GET: api/SupportAttachment/ticket/1
    [HttpGet("ticket/{ticketId}")]
    public async Task<IActionResult> GetAttachmentsByTicketId(int ticketId)
    {
      var attachments = await service.GetAttachmentsByTicketId(ticketId);
      return Ok(attachments);
    }

    // POST: api/SupportAttachment
    [HttpPost]
    public async Task<IActionResult> CreateAttachment(SupportAttachmentDto attachment)
    {
      var createdAttachment = await service.CreateAttachment(attachment);

      return CreatedAtAction(
          nameof(GetAttachmentById),
          new { id = createdAttachment.AttachmentId },
          createdAttachment);
    }

    // PUT: api/SupportAttachment/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttachment(int id, SupportAttachmentDto attachment)
    {
      var updatedAttachment = await service.UpdateAttachment(id, attachment);

      if (updatedAttachment == null)
        return NotFound("Attachment not found.");

      return Ok(updatedAttachment);
    }

    // DELETE: api/SupportAttachment/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(int id)
    {
      var deleted = await service.DeleteAttachment(id);

      if (!deleted)
        return NotFound("Attachment not found.");

      return Ok("Attachment deleted successfully.");
    }
  }
}
