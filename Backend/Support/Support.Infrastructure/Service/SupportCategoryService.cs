
using Microsoft.EntityFrameworkCore;
using Support.Application.DTOs;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Services
{
  public class SupportCategoryService : ISupportCategoryService
  {
    private readonly AppDbContext db;

    public SupportCategoryService(AppDbContext db)
    {
      this.db = db;
    }

    // 1. Get all categories
    public async Task<List<SupportCategoryDto>> GetAllCategories()
    {
      return await db.SupportCategories
          .Select(c => new SupportCategoryDto
          {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description,
            IsActive = c.IsActive
          })
          .ToListAsync();
    }

    // 2. Get category by ID
    public async Task<SupportCategoryDto?> GetCategoryById(int id)
    {
      var c = await db.SupportCategories.FindAsync(id);

      if (c == null)
        return null;

      return new SupportCategoryDto
      {
        CategoryId = c.CategoryId,
        CategoryName = c.CategoryName,
        Description = c.Description,
        IsActive = c.IsActive
      };
    }

    // 3. Create category
    public async Task<SupportCategoryDto> CreateCategory(SupportCategoryDto category)
    {
      var newCategory = new SupportCategory
      {
        CategoryName = category.CategoryName,
        Description = category.Description,
        IsActive = category.IsActive
      };

      db.SupportCategories.Add(newCategory);
      await db.SaveChangesAsync();

      category.CategoryId = newCategory.CategoryId;

      return category;
    }

    // 4. Update category
    public async Task<SupportCategoryDto?> UpdateCategory(int id, SupportCategoryDto category)
    {
      var existingCategory = await db.SupportCategories.FindAsync(id);

      if (existingCategory == null)
        return null;

      existingCategory.CategoryName = category.CategoryName;
      existingCategory.Description = category.Description;
      existingCategory.IsActive = category.IsActive;

      await db.SaveChangesAsync();

      return await GetCategoryById(id);
    }

    // 5. Delete category
    public async Task<bool> DeleteCategory(int id)
    {
      var category = await db.SupportCategories.FindAsync(id);

      if (category == null)
        return false;

      db.SupportCategories.Remove(category);
      await db.SaveChangesAsync();

      return true;
    }
  }
}
