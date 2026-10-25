
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductCommissionController : ControllerBase
    {
        private readonly IProductCommissionService service;

        public ProductCommissionController(
            IProductCommissionService service)
        {
            this.service = service;
        }

        [HttpPost("api/v1/products/{id:int}/commission")]
        public async Task<IActionResult> AddCommission(
            int id, [FromBody] product_commissionDTO dto)
        {
            try
            {
                await service.AddCommission(id, dto);

                return Ok(new { msg = "Commission Added" });
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

        [HttpGet("api/v1/products/{id:int}/commission")]
        public async Task<IActionResult> FindCommissionByProductId(int id)
        {
            try
            {
                var data = await service.FindCommissionByProductId(id);

                if (data == null)
                    return NotFound(new { msg = "Commission not found." });

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("api/v1/product-commissions/{commissionId:int}")]
        public async Task<IActionResult> UpdateCommission(
            int commissionId,
            [FromBody] product_commissionDTO dto)
        {
            try
            {
                await service.UpdateCommission(commissionId, dto);

                return Ok(new { msg = "Commission Updated" });
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

        [HttpDelete("api/v1/product-commissions/{commissionId:int}")]
        public async Task<IActionResult> DeleteCommission(int commissionId)
        {
            try
            {
                await service.DeleteCommission(commissionId);

                return Ok(new { msg = "Commission Deleted" });
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
    }
}
