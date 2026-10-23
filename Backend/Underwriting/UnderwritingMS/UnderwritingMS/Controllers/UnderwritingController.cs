
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Controllers
{
    [Route("api/v1/underwriting")]
    [ApiController]
    public class UnderwritingController : ControllerBase
    {
        IUnderwritingService service;

        public UnderwritingController(IUnderwritingService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase(createUnderwritingDTO dto)
        {
            try
            {
                await service.CreateCase(dto);
                return Ok(new { msg = "Underwriting case created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllCases()
        {
            var data = await service.FetchAllCases();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindCaseById(int id)
        {
            var data = await service.FindCaseById(id);

            if (data == null)
                return NotFound(new { msg = "Underwriting case not found" });

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase(
            int id, createUnderwritingDTO dto)
        {
            try
            {
                await service.UpdateCase(id, dto);
                return Ok(new { msg = "Underwriting case updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
