
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [Route("api/v1/products")]
    [ApiController]
    public class InsuranceProductController : ControllerBase
    {
        IInsuranceProductService service;

        public InsuranceProductController(IInsuranceProductService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(insurance_productDTO dto)
        {
            try
            {
                await service.AddProduct(dto);
                return Ok(new { msg = "Added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindProductById(int id)
        {
            var data = await service.FindProductById(id);

            if (data == null)
                return NotFound(new { msg = "Product not found" });

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllProducts()
        {
            var data = await service.FetchAllProducts();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id, insurance_productDTO dto)
        {
            try
            {
                await service.UpdateProduct(id, dto);
                return Ok(new { msg = "Updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateProduct(int id)
        {
            try
            {
                await service.DeactivateProduct(id);
                return Ok(new { msg = "Deactivated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
