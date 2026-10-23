
using AuditLogs.Application.DTO;
using AuditLogs.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditLogs.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuditLogsController : ControllerBase
  {
    private readonly IAuditLogService service;

    public AuditLogsController(IAuditLogService service)
    {
      this.service = service;
    }

    // 1. Get All Audit Logs
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var auditLogs = await service.GetAllAsync();

      return Ok(auditLogs);
    }

    // 2. Get Audit Log By ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var auditLog = await service.GetByIdAsync(id);

      if (auditLog == null)
      {
        return NotFound("Audit log not found.");
      }

      return Ok(auditLog);
    }

    // 3. Get Audit Logs By Customer ID
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomerId(int customerId)
    {
      var auditLogs = await service.GetByCustomerIdAsync(customerId);

      return Ok(auditLogs);
    }

    // 4. Add New Audit Log
    [HttpPost]
    public async Task<IActionResult> Add(AuditLogDto auditLogDto)
    {
      var auditLog = await service.AddAsync(auditLogDto);

      return CreatedAtAction(
          nameof(GetById),
          new { id = auditLog.AuditLogId },
          auditLog
      );
    }

    // 5. Update Audit Log
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AuditLogDto auditLogDto)
    {
      var result = await service.UpdateAsync(id, auditLogDto);

      if (!result)
      {
        return NotFound("Audit log not found.");
      }

      return Ok("Audit log updated successfully.");
    }

    // 6. Delete Audit Log
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await service.DeleteAsync(id);

      if (!result)
      {
        return NotFound("Audit log not found.");
      }

      return Ok("Audit log deleted successfully.");
    }



    [HttpPost("log")]
    public async Task<IActionResult> LogAction(
        [FromBody] AuditLogRequestDto request)
    {
      await service.LogActionAsync(
          request.CustomerId,
          request.Action,
          request.EntityName,
          request.EntityId,
          request.Description);

      return Ok("Audit log saved successfully.");
    }


  }
}
