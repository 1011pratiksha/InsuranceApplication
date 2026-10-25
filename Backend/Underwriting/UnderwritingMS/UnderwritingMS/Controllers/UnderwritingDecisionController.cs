
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace UnderwritingMS.Controllers
{
    [Authorize]
    [Route("api/v1/underwriting/{id}")]
    [ApiController]
    public class UnderwritingDecisionController : ControllerBase
    {
        IUnderwritingDecisionService service;

        public UnderwritingDecisionController(
            IUnderwritingDecisionService service)
        {
            this.service = service;
        }

        [HttpPut("approve")]
        public async Task<IActionResult> ApproveCase(
            int id, createUnderwritingDecisionDTO dto)
        {
            try
            {
                await service.ApproveCase(id, dto);
                return Ok(new { msg = "Underwriting case approved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("reject")]
        public async Task<IActionResult> RejectCase(
            int id, createUnderwritingDecisionDTO dto)
        {
            try
            {
                await service.RejectCase(id, dto);
                return Ok(new { msg = "Underwriting case rejected" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("loading")]
        public async Task<IActionResult> ApplyLoading(
            int id, createUnderwritingDecisionDTO dto)
        {
            try
            {
                await service.ApplyLoading(id, dto);
                return Ok(new { msg = "Premium loading applied" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("decision")]
        public async Task<IActionResult> GetDecision(int id)
        {
            var data = await service.GetDecision(id);

            if (data == null)
                return NotFound(new { msg = "Decision not found" });

            return Ok(data);
        }
    }
}
