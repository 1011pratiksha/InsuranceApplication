
using Support.Application.DTOs;

namespace Support.Application.Interfaces
{
  public interface ISupportCategoryService
  {
    Task<List<SupportCategoryDto>> GetAllCategories();

    Task<SupportCategoryDto?> GetCategoryById(int id);

    Task<SupportCategoryDto> CreateCategory(SupportCategoryDto category);

    Task<SupportCategoryDto?> UpdateCategory(int id, SupportCategoryDto category);

    Task<bool> DeleteCategory(int id);
  }
}
