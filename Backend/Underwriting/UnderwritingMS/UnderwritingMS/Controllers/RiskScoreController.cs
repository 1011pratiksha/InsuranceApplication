
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Controllers
{
    [Route("api/v1/underwriting/{id}/risk-score")]
    [ApiController]
    public class RiskScoreController : ControllerBase
    {
        IRiskScoreService service;

        public RiskScoreController(IRiskScoreService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateRiskScore(
            int id, calculateRiskScoreDTO dto)
        {
            try
            {
                await service.CalculateRiskScore(id, dto);

                return Ok(new { msg = "Risk score calculated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRiskScore(int id)
        {
            var data = await service.GetRiskScore(id);

            if (data == null)
                return NotFound(new { msg = "Risk score not found" });

            return Ok(data);
        }
    }
}
