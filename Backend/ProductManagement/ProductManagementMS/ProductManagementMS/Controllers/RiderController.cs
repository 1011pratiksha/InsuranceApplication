
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly IRiderService service;

        public RiderController(IRiderService service)
        {
            this.service = service;
        }

        [HttpPost("api/v1/products/{id:int}/riders")]
        public async Task<IActionResult> AddRider(
            int id, [FromBody] rider_masterDTO dto)
        {
            try
            {
                await service.AddRider(id, dto);
                return Ok(new { msg = "Rider Added" });
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

        [HttpGet("api/v1/products/{id:int}/riders")]
        public async Task<IActionResult> FetchRidersByProductId(int id)
        {
            try
            {
                var data = await service.FetchRidersByProductId(id);
                return Ok(data);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpPut("api/v1/riders/{riderId:int}")]
        public async Task<IActionResult> UpdateRider(
            int riderId, [FromBody] rider_masterDTO dto)
        {
            try
            {
                await service.UpdateRider(riderId, dto);
                return Ok(new { msg = "Rider Updated" });
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

        [HttpDelete("api/v1/riders/{riderId:int}")]
        public async Task<IActionResult> DeleteRider(int riderId)
        {
            try
            {
                await service.DeleteRider(riderId);
                return Ok(new { msg = "Rider Deleted" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
