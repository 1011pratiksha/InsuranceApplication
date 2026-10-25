
using Microsoft.AspNetCore.Mvc;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementMS.Controllers
{
    [Authorize]
    [Route("api/v1/product-categories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService service;

        public ProductCategoryController(IProductCategoryService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(product_categoryDTO dto)
        {
            try
            {
                await service.AddCategory(dto);
                return Ok(new { msg = "Added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> FindCategoryById(int id)
        {
            var data = await service.FindCategoryById(id);

            if (data == null)
                return NotFound(new { msg = "Category not found" });

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllCategories()
        {
            var data = await service.FetchAllCategories();
            return Ok(data);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(
            int id, product_categoryDTO dto)
        {
            try
            {
                await service.UpdateCategory(id, dto);
                return Ok(new { msg = "Updated" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeactivateCategory(int id)
        {
            try
            {
                await service.DeactivateCategory(id);
                return Ok(new { msg = "Deactivated" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
