
using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Interfaces;

namespace Support.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupportCategoryController : ControllerBase
  {
    private readonly ISupportCategoryService service;

    public SupportCategoryController(ISupportCategoryService service)
    {
      this.service = service;
    }

    // GET: api/SupportCategory
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
      var categories = await service.GetAllCategories();
      return Ok(categories);
    }

    // GET: api/SupportCategory/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
      var category = await service.GetCategoryById(id);

      if (category == null)
        return NotFound("Category not found.");

      return Ok(category);
    }

    // POST: api/SupportCategory
    [HttpPost]
    public async Task<IActionResult> CreateCategory(SupportCategoryDto category)
    {
      var createdCategory = await service.CreateCategory(category);

      return CreatedAtAction(
          nameof(GetCategoryById),
          new { id = createdCategory.CategoryId },
          createdCategory);
    }

    // PUT: api/SupportCategory/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, SupportCategoryDto category)
    {
      var updatedCategory = await service.UpdateCategory(id, category);

      if (updatedCategory == null)
        return NotFound("Category not found.");

      return Ok(updatedCategory);
    }

    // DELETE: api/SupportCategory/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
      var deleted = await service.DeleteCategory(id);

      if (!deleted)
        return NotFound("Category not found.");

      return Ok("Category deleted successfully.");
    }
  }
}
