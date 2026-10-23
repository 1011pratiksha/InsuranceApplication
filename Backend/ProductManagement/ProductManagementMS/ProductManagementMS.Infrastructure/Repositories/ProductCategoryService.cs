
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public ProductCategoryService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddCategory(product_categoryDTO dto)
        {
            if (dto == null)
                throw new Exception("Category data is required.");

            if (string.IsNullOrWhiteSpace(dto.category_name))
                throw new Exception("Category name is required.");

            string name = dto.category_name.Trim();

            if (name.Length > 100)
                throw new Exception("Category name cannot exceed 100 characters.");

            if (dto.description?.Length > 255)
                throw new Exception("Description cannot exceed 255 characters.");

            bool exists = await db.product_category
                .AnyAsync(x => x.category_name.ToLower() == name.ToLower());

            if (exists)
                throw new Exception("Category name already exists.");

            var d = mapper.Map<product_category>(dto);

            d.category_id = 0;
            d.category_name = name;
            d.status = "Active";

            await db.product_category.AddAsync(d);
            await db.SaveChangesAsync();
        }

        public async Task<List<product_categoryDTO>> FetchAllCategories()
        {
            var categories = await db.product_category
                .AsNoTracking()
                .OrderBy(x => x.category_name)
                .ToListAsync();

            return mapper.Map<List<product_categoryDTO>>(categories);
        }

        public async Task<product_categoryDTO?> FindCategoryById(int id)
        {
            var category = await db.product_category
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.category_id == id);

            return category == null
                ? null
                : mapper.Map<product_categoryDTO>(category);
        }

        public async Task UpdateCategory(int id, product_categoryDTO dto)
        {
            if (dto == null)
                throw new Exception("Category data is required.");

            if (string.IsNullOrWhiteSpace(dto.category_name))
                throw new Exception("Category name is required.");

            string name = dto.category_name.Trim();

            if (name.Length > 100)
                throw new Exception("Category name cannot exceed 100 characters.");

            if (dto.description?.Length > 255)
                throw new Exception("Description cannot exceed 255 characters.");

            if (dto.status != "Active" && dto.status != "Inactive")
                throw new Exception("Status must be Active or Inactive.");

            var category = await db.product_category.FindAsync(id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            bool duplicate = await db.product_category.AnyAsync(
                x => x.category_id != id &&
                     x.category_name.ToLower() == name.ToLower());

            if (duplicate)
                throw new Exception("Category name already exists.");

            category.category_name = name;
            category.description = dto.description;
            category.status = dto.status;

            await db.SaveChangesAsync();
        }

        public async Task DeactivateCategory(int id)
        {
            var category = await db.product_category.FindAsync(id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            bool hasActiveProducts = await db.insurance_product.AnyAsync(
                x => x.category_id == id && x.status == "Active");

            if (hasActiveProducts)
                throw new InvalidOperationException(
                    "Cannot deactivate a category that has active products.");

            category.status = "Inactive";

            await db.SaveChangesAsync();
        }
    }
}
