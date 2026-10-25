
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [ApiController]
    public class CoverageController : ControllerBase
    {
        private readonly ICoverageService service;

        public CoverageController(ICoverageService service)
        {
            this.service = service;
        }

        [HttpPost("api/v1/products/{id:int}/coverages")]
        public async Task<IActionResult> AddCoverage(
            int id, [FromBody] coverage_masterDTO dto)
        {
            try
            {
                await service.AddCoverage(id, dto);
                return Ok(new { msg = "Coverage Added" });
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

        [HttpGet("api/v1/products/{id:int}/coverages")]
        public async Task<IActionResult> FetchCoveragesByProductId(int id)
        {
            try
            {
                var data = await service.FetchCoveragesByProductId(id);
                return Ok(data);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpPut("api/v1/coverages/{coverageId:int}")]
        public async Task<IActionResult> UpdateCoverage(
            int coverageId, [FromBody] coverage_masterDTO dto)
        {
            try
            {
                await service.UpdateCoverage(coverageId, dto);
                return Ok(new { msg = "Coverage Updated" });
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

        [HttpDelete("api/v1/coverages/{coverageId:int}")]
        public async Task<IActionResult> DeleteCoverage(int coverageId)
        {
            try
            {
                await service.DeleteCoverage(coverageId);
                return Ok(new { msg = "Coverage Deleted" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
