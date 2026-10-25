
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/premium-frequencies")]
    public class PremiumFrequencyController : ControllerBase
    {
        private readonly IPremiumFrequencyService service;

        public PremiumFrequencyController(IPremiumFrequencyService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddFrequency(
            [FromBody] premium_frequencyDTO dto)
        {
            try
            {
                await service.AddFrequency(dto);
                return Ok(new { msg = "Premium Frequency Added" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllFrequencies()
        {
            var data = await service.FetchAllFrequencies();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> FindFrequencyById(int id)
        {
            var data = await service.FindFrequencyById(id);

            if (data == null)
                return NotFound(new { msg = "Premium frequency not found." });

            return Ok(data);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFrequency(
            int id, [FromBody] premium_frequencyDTO dto)
        {
            try
            {
                await service.UpdateFrequency(id, dto);
                return Ok(new { msg = "Premium Frequency Updated" });
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeactivateFrequency(int id)
        {
            try
            {
                await service.DeactivateFrequency(id);
                return Ok(new { msg = "Premium Frequency Deactivated" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
