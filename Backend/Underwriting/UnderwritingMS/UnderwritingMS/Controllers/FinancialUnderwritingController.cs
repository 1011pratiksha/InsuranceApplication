
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Controllers
{
    [Route("api/v1/underwriting/{id}/financial")]
    [ApiController]
    public class FinancialUnderwritingController : ControllerBase
    {
        IFinancialUnderwritingService service;

        public FinancialUnderwritingController(
            IFinancialUnderwritingService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddFinancialDetails(
            int id, financial_underwritingDTO dto)
        {
            try
            {
                await service.AddFinancialDetails(id, dto);
                return Ok(new { msg = "Financial details added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFinancialDetails(int id)
        {
            var data = await service.GetFinancialDetails(id);

            if (data == null)
                return NotFound(new { msg = "Financial details not found" });

            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFinancialDetails(
            int id, financial_underwritingDTO dto)
        {
            try
            {
                await service.UpdateFinancialDetails(id, dto);
                return Ok(new { msg = "Financial details updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
