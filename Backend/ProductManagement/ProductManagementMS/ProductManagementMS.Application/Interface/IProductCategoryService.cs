
using ProductManagementMS.Application.DTO;

namespace ProductManagementMS.Application.Interface
{
    public interface IProductCategoryService
    {
        Task AddCategory(product_categoryDTO dto);

        Task<List<product_categoryDTO>> FetchAllCategories();

        Task<product_categoryDTO?> FindCategoryById(int id);

        Task UpdateCategory(int id, product_categoryDTO dto);

        Task DeactivateCategory(int id);
    }
}
