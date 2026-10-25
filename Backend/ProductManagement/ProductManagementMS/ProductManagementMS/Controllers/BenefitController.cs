using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize] // Add this line
    [ApiController]
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitService service;

        public BenefitController(IBenefitService service)
        {
            this.service = service;
        }

        [HttpPost("api/v1/products/{id:int}/benefits")]
        public async Task<IActionResult> AddBenefit(
            int id, [FromBody] benefit_masterDTO dto)
        {
            try
            {
                await service.AddBenefit(id, dto);
                return Ok(new { msg = "Benefit Added" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("api/v1/products/{id:int}/benefits")]
        public async Task<IActionResult> FetchBenefitsByProductId(int id)
        {
            try
            {
                var data = await service.FetchBenefitsByProductId(id);
                return Ok(data);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpPut("api/v1/benefits/{benefitId:int}")]
        public async Task<IActionResult> UpdateBenefit(
            int benefitId, [FromBody] benefit_masterDTO dto)
        {
            try
            {
                await service.UpdateBenefit(benefitId, dto);
                return Ok(new { msg = "Benefit Updated" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("api/v1/benefits/{benefitId:int}")]
        public async Task<IActionResult> DeleteBenefit(int benefitId)
        {
            try
            {
                await service.DeleteBenefit(benefitId);
                return Ok(new { msg = "Benefit Deleted" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}