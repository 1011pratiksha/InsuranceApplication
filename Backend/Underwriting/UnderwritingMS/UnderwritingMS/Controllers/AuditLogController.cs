
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Controllers
{
    [Route("api/v1/underwriting/{id}/audit-log")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        IAuditLogService service;

        public AuditLogController(IAuditLogService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs(int id)
        {
            try
            {
                var data = await service.GetAuditLogs(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Underwriting case not found.")
                    return NotFound(new { msg = ex.Message });

                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
